using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAppCourierTrack.DTO;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Todos los endpoints requieren autenticación
    public class DetallePedidoController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public DetallePedidoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/DetallePedido (solo administrador)
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<List<DetallePedidoDTO>>> Get()
        {
            var detalles = await _context.DetallePedidos.ToListAsync();
            return Ok(_mapper.Map<List<DetallePedidoDTO>>(detalles));
        }

        // GET: api/DetallePedido/5
        [HttpGet("{id:int}", Name = "ObtenerDetallePedido")]
        public async Task<ActionResult<DetallePedidoDTO>> Get(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            var detalle = await _context.DetallePedidos
                .Include(d => d.Pedidos) // para verificar relación con pedidos
                .FirstOrDefaultAsync(d => d.Id == id);

            if (detalle == null)
                return NotFound("No existe el detalle de pedido");

            // Verificar permisos: administrador o usuario relacionado a través de pedidos
            if (rol != "ADMINISTRADOR")
            {
                // Obtener los clientes asociados a los pedidos que usan este detalle
                var clienteIds = await _context.Pedidos
                    .Where(p => p.DetallePedidoId == id)
                    .Select(p => p.ClienteId)
                    .Distinct()
                    .ToListAsync();

                var usuariosIds = await _context.Clientes
                    .Where(c => clienteIds.Contains(c.Id))
                    .Select(c => c.UsuarioId)
                    .ToListAsync();

                if (!usuariosIds.Contains(userId))
                    return Forbid("No tienes permiso para ver este detalle de pedido.");
            }

            return Ok(_mapper.Map<DetallePedidoDTO>(detalle));
        }

        // POST: api/DetallePedido (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<DetallePedidoDTO>> Post(DetallePedidoCreaDTO dto)
        {
            // Verificar que la DireccionOrigen exista
            if (!await _context.DireccionesOrigenes.AnyAsync(d => d.Id == dto.DireccionOrigenId))
                return BadRequest($"La dirección origen con Id {dto.DireccionOrigenId} no existe.");

            // Verificar que la DireccionDestino exista
            if (!await _context.DireccionesDestinos.AnyAsync(d => d.Id == dto.DireccionDestinoId))
                return BadRequest($"La dirección destino con Id {dto.DireccionDestinoId} no existe.");

            var detalle = _mapper.Map<DetallePedido>(dto);
            _context.DetallePedidos.Add(detalle);
            await _context.SaveChangesAsync();

            var detalleDTO = _mapper.Map<DetallePedidoDTO>(detalle);
            return CreatedAtRoute("ObtenerDetallePedido", new { id = detalle.Id }, detalleDTO);
        }

        // PUT: api/DetallePedido/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, DetallePedidoCreaDTO dto)
        {
            var detalle = await _context.DetallePedidos.FindAsync(id);
            if (detalle == null)
                return NotFound($"No existe el detalle de pedido con Id {id}");

            // Validar existencia de direcciones si cambian
            if (detalle.DireccionOrigenId != dto.DireccionOrigenId)
            {
                if (!await _context.DireccionesOrigenes.AnyAsync(d => d.Id == dto.DireccionOrigenId))
                    return BadRequest($"La dirección origen con Id {dto.DireccionOrigenId} no existe.");
            }

            if (detalle.DireccionDestinoId != dto.DireccionDestinoId)
            {
                if (!await _context.DireccionesDestinos.AnyAsync(d => d.Id == dto.DireccionDestinoId))
                    return BadRequest($"La dirección destino con Id {dto.DireccionDestinoId} no existe.");
            }

            _mapper.Map(dto, detalle);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/DetallePedido/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var detalle = await _context.DetallePedidos.FindAsync(id);
            if (detalle == null)
                return NotFound("No existe el detalle de pedido");

            // Verificar si hay pedidos asociados
            var tienePedido = await _context.Pedidos.AnyAsync(p => p.DetallePedidoId == id);
            if (tienePedido)
                return BadRequest("No se puede eliminar el detalle de pedido porque hay pedidos asociados.");

            _context.DetallePedidos.Remove(detalle);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}