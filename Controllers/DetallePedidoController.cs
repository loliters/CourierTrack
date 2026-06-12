using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCourierTrack.DTO;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidoController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public DetallePedidoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/DetallePedido 
        [HttpGet]
        public async Task<ActionResult<List<DetallePedidoDTO>>> Get()
        {
            var detalles = await _context.DetallePedidos.ToListAsync();
            return Ok(_mapper.Map<List<DetallePedidoDTO>>(detalles));
        }

        // GET: api/DetallePedido/5 
        [HttpGet("{id:int}", Name = "ObtenerDetallePedido")]
        public async Task<ActionResult<DetallePedidoDTO>> Get(int id)
        {
            var detalle = await _context.DetallePedidos.FindAsync(id);
            if (detalle == null)
                return NotFound("No existe el detalle de pedido");

            return Ok(_mapper.Map<DetallePedidoDTO>(detalle));
        }

        // POST: api/DetallePedido (solo Administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<DetallePedidoDTO>> Post(DetallePedidoCreaDTO detallePedidoCreaDTO)
        {
            // Verificar que la DireccionOrigen exista
            var direccionOrigenExiste = await _context.DireccionesOrigenes.AnyAsync(d => d.Id == detallePedidoCreaDTO.DireccionOrigenId);
            if (!direccionOrigenExiste)
                return BadRequest($"La dirección origen con Id {detallePedidoCreaDTO.DireccionOrigenId} no existe.");

            // Verificar que la DireccionDestino exista
            var direccionDestinoExiste = await _context.DireccionesDestinos.AnyAsync(d => d.Id == detallePedidoCreaDTO.DireccionDestinoId);
            if (!direccionDestinoExiste)
                return BadRequest($"La dirección destino con Id {detallePedidoCreaDTO.DireccionDestinoId} no existe.");

            var detalle = _mapper.Map<DetallePedido>(detallePedidoCreaDTO);
            _context.DetallePedidos.Add(detalle);
            await _context.SaveChangesAsync();

            var detalleDTO = _mapper.Map<DetallePedidoDTO>(detalle);
            return CreatedAtRoute("ObtenerDetallePedido", new { id = detalle.Id }, detalleDTO);
        }

        // PUT: api/DetallePedido/5 (solo Administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, DetallePedidoCreaDTO detallePedidoCreaDTO)
        {
            var detalle = await _context.DetallePedidos.FindAsync(id);
            if (detalle == null)
                return NotFound($"No existe el detalle de pedido con Id {id}");

            // Validar existencia de direcciones si cambian
            if (detalle.DireccionOrigenId != detallePedidoCreaDTO.DireccionOrigenId)
            {
                var direccionOrigenExiste = await _context.DireccionesOrigenes.AnyAsync(d => d.Id == detallePedidoCreaDTO.DireccionOrigenId);
                if (!direccionOrigenExiste)
                    return BadRequest($"La dirección origen con Id {detallePedidoCreaDTO.DireccionOrigenId} no existe.");
            }

            if (detalle.DireccionDestinoId != detallePedidoCreaDTO.DireccionDestinoId)
            {
                var direccionDestinoExiste = await _context.DireccionesDestinos.AnyAsync(d => d.Id == detallePedidoCreaDTO.DireccionDestinoId);
                if (!direccionDestinoExiste)
                    return BadRequest($"La dirección destino con Id {detallePedidoCreaDTO.DireccionDestinoId} no existe.");
            }

            _mapper.Map(detallePedidoCreaDTO, detalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/DetallePedido/5 (solo Administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var detalle = await _context.DetallePedidos.FindAsync(id);
            if (detalle == null)
                return NotFound("No existe el detalle de pedido");

            // Verificar si hay pedidos asociados a este detalle
            var tienePedido = await _context.Pedidos.AnyAsync(p => p.DetallePedidoId == id);
            if (tienePedido)
                return BadRequest("No se puede eliminar el detalle de pedido porque hay pedidos asociados.");

            _context.DetallePedidos.Remove(detalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
