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
    [Authorize]
    public class DireccionDestinoController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public DireccionDestinoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/DireccionDestino (solo administrador)
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<List<DireccionDestinoDTO>>> Get()
        {
            var direcciones = await _context.DireccionesDestinos.ToListAsync();
            return Ok(_mapper.Map<List<DireccionDestinoDTO>>(direcciones));
        }

        // GET: api/DireccionDestino/mis-direcciones (cliente autenticado)
        [HttpGet("mis-direcciones")]
        public async Task<ActionResult<List<DireccionDestinoDTO>>> GetMisDirecciones()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Obtener IDs de direcciones destino usadas en pedidos del usuario
            var direccionesIds = await _context.DetallePedidos
                .Where(dp => _context.Pedidos.Any(p => p.DetallePedidoId == dp.Id &&
                            _context.Clientes.Any(c => c.Id == p.ClienteId && c.UsuarioId == userId)))
                .Select(dp => dp.DireccionDestinoId)
                .Distinct()
                .ToListAsync();

            var direcciones = await _context.DireccionesDestinos
                .Where(d => direccionesIds.Contains(d.Id))
                .ToListAsync();

            return Ok(_mapper.Map<List<DireccionDestinoDTO>>(direcciones));
        }

        // GET: api/DireccionDestino/5 (permiso si está relacionada con el usuario o es admin)
        [HttpGet("{id:int}", Name = "ObtenerDireccionDestino")]
        public async Task<ActionResult<DireccionDestinoDTO>> Get(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            var direccion = await _context.DireccionesDestinos.FindAsync(id);
            if (direccion == null)
                return NotFound("Dirección destino no encontrada.");

            if (rol != "ADMINISTRADOR")
            {
                var puedeVer = await _context.DetallePedidos
                    .AnyAsync(dp => dp.DireccionDestinoId == id &&
                        _context.Pedidos.Any(p => p.DetallePedidoId == dp.Id &&
                            _context.Clientes.Any(c => c.Id == p.ClienteId && c.UsuarioId == userId)));

                if (!puedeVer)
                    return Forbid("No tienes permiso para ver esta dirección destino.");
            }

            return Ok(_mapper.Map<DireccionDestinoDTO>(direccion));
        }

        // POST: api/DireccionDestino (cualquier usuario autenticado puede crear)
        [HttpPost]
        public async Task<ActionResult<DireccionDestinoDTO>> Post(DireccionDestinoCreaDTO dto)
        {
            if (!await _context.Ubicaciones.AnyAsync(u => u.Id == dto.UbicacionId))
                return BadRequest("La ubicación no existe.");

            var direccion = _mapper.Map<DireccionDestino>(dto);
            _context.DireccionesDestinos.Add(direccion);
            await _context.SaveChangesAsync();

            var direccionDTO = _mapper.Map<DireccionDestinoDTO>(direccion);
            return CreatedAtRoute("ObtenerDireccionDestino", new { id = direccion.Id }, direccionDTO);
        }

        // PUT: api/DireccionDestino/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, DireccionDestinoCreaDTO dto)
        {
            var direccion = await _context.DireccionesDestinos.FindAsync(id);
            if (direccion == null)
                return NotFound("La dirección destino no existe.");

            if (!await _context.Ubicaciones.AnyAsync(u => u.Id == dto.UbicacionId))
                return BadRequest("La ubicación no existe.");

            _mapper.Map(dto, direccion);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/DireccionDestino/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var direccion = await _context.DireccionesDestinos.FindAsync(id);
            if (direccion == null)
                return NotFound("La dirección destino no existe.");

            var tieneDetalles = await _context.DetallePedidos.AnyAsync(dp => dp.DireccionDestinoId == id);
            if (tieneDetalles)
                return BadRequest("No se puede eliminar porque tiene detalles de pedido asociados.");

            _context.DireccionesDestinos.Remove(direccion);
            await _context.SaveChangesAsync();
            return Ok("Dirección destino eliminada correctamente.");
        }
    }
}