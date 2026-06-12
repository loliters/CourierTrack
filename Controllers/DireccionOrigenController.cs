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
    public class DireccionOrigenController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public DireccionOrigenController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/DireccionOrigen (solo administrador)
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<List<DireccionOrigenDTO>>> Get()
        {
            var direcciones = await _context.DireccionesOrigenes.ToListAsync();
            return Ok(_mapper.Map<List<DireccionOrigenDTO>>(direcciones));
        }

        // GET: api/DireccionOrigen/mis-direcciones (cliente autenticado)
        [HttpGet("mis-direcciones")]
        public async Task<ActionResult<List<DireccionOrigenDTO>>> GetMisDirecciones()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Obtener IDs de direcciones origen usadas en pedidos del usuario
            var direccionesIds = await _context.DetallePedidos
                .Where(dp => _context.Pedidos.Any(p => p.DetallePedidoId == dp.Id &&
                            _context.Clientes.Any(c => c.Id == p.ClienteId && c.UsuarioId == userId)))
                .Select(dp => dp.DireccionOrigenId)
                .Distinct()
                .ToListAsync();

            var direcciones = await _context.DireccionesOrigenes
                .Where(d => direccionesIds.Contains(d.Id))
                .ToListAsync();

            return Ok(_mapper.Map<List<DireccionOrigenDTO>>(direcciones));
        }

        // GET: api/DireccionOrigen/5 (permiso si está relacionada con el usuario o es admin)
        [HttpGet("{id:int}", Name = "ObtenerDireccionOrigen")]
        public async Task<ActionResult<DireccionOrigenDTO>> Get(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            var direccion = await _context.DireccionesOrigenes.FindAsync(id);
            if (direccion == null)
                return NotFound("Dirección origen no encontrada.");

            if (rol != "ADMINISTRADOR")
            {
                var puedeVer = await _context.DetallePedidos
                    .AnyAsync(dp => dp.DireccionOrigenId == id &&
                        _context.Pedidos.Any(p => p.DetallePedidoId == dp.Id &&
                            _context.Clientes.Any(c => c.Id == p.ClienteId && c.UsuarioId == userId)));

                if (!puedeVer)
                    return Forbid("No tienes permiso para ver esta dirección origen.");
            }

            return Ok(_mapper.Map<DireccionOrigenDTO>(direccion));
        }

        // POST: api/DireccionOrigen (cualquier usuario autenticado puede crear)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Post(
            [FromBody]
            DireccionOrigenCreaDTO
            direccionOrigenCreaDTO)
        {
            // Validar ubicación
            if (!await _context.Ubicaciones.AnyAsync(u => u.Id == dto.UbicacionId))
                return BadRequest("La ubicación no existe.");

            var direccion = _mapper.Map<DireccionOrigen>(dto);
            _context.DireccionesOrigenes.Add(direccion);
            await _context.SaveChangesAsync();

            var direccionDTO =
                _mapper.Map<
                    DireccionOrigenDTO>(
                    direccion);

            return CreatedAtRoute("ObtenerDireccionOrigen",
                new
                {
                    id = direccion.Id
                },
                direccionDTO);
        }

        // PUT: api/DireccionOrigen/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Put(
            int id,
            DireccionOrigenCreaDTO
            direccionOrigenCreaDTO)
        {
            var existeDireccion =
                await _context
                .DireccionesOrigenes
                .AnyAsync(x =>
                    x.Id == id);

            if (!existeDireccion)
            {
                return NotFound(
                    "La dirección origen no existe.");
            }

            // validar ubicación
            var existeUbicacion =
                await _context.Ubicaciones
                .AnyAsync(x =>
                    x.Id ==
                    direccionOrigenCreaDTO
                    .UbicacionId);

            if (!existeUbicacion)
            {
                return BadRequest(
                    "La ubicación no existe.");
            }

            var direccion =
                _mapper.Map<
                    DireccionOrigen>(
                    direccionOrigenCreaDTO);

            direccion.Id = id;

            _context.Update(
                direccion);

            if (!await _context.Ubicaciones.AnyAsync(u => u.Id == dto.UbicacionId))
                return BadRequest("La ubicación no existe.");

            _mapper.Map(dto, direccion);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/DireccionOrigen/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult>
            Delete(int id)
        {
            var direccion = await _context.DireccionesOrigenes.FindAsync(id);
            if (direccion == null)
                return NotFound("La dirección origen no existe.");

            // Verificar si tiene detalles de pedido asociados
            var tieneDetalles = await _context.DetallePedidos.AnyAsync(dp => dp.DireccionOrigenId == id);
            if (tieneDetalles)
                return BadRequest("No se puede eliminar porque tiene detalles de pedido asociados.");

            _context.DireccionesOrigenes.Remove(direccion);
            await _context.SaveChangesAsync();
            return Ok("Dirección origen eliminada correctamente.");
        }
    }
}