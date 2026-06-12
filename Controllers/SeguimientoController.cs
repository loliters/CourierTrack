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
    public class SeguimientoController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public SeguimientoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Seguimiento
        [HttpGet]
        public async Task<ActionResult<List<SeguimientoDTO>>> Get()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            IQueryable<Seguimiento> query = _context.Seguimientos
                .Include(s => s.Pedido)
                    .ThenInclude(p => p.Cliente)
                .Include(s => s.Conductor)
                    .ThenInclude(c => c.Usuario)
                .Include(s => s.Vehiculo)
                .Include(s => s.Ubicacion);

            if (rol == "ADMINISTRADOR")
            {
                // Admin ve todos
            }
            else if (rol == "CLIENTE")
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.UsuarioId == userId);
                if (cliente == null)
                    return BadRequest("No tienes un perfil de cliente.");
                query = query.Where(s => s.Pedido.ClienteId == cliente.Id);
            }
            else if (rol == "CONDUCTOR")
            {
                var conductor = await _context.Conductores.FirstOrDefaultAsync(c => c.UsuarioId == userId);
                if (conductor == null)
                    return BadRequest("No tienes un perfil de conductor.");
                query = query.Where(s => s.ConductorId == conductor.Id);
            }
            else
            {
                return Forbid("Rol no autorizado.");
            }

            var seguimientos = await query.ToListAsync();
            return Ok(_mapper.Map<List<SeguimientoDTO>>(seguimientos));
        }

        // GET: api/Seguimiento/5
        [HttpGet("{id:int}", Name = "ObtenerSeguimiento")]
        public async Task<ActionResult<SeguimientoDTO>> Get(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            var seguimiento = await _context.Seguimientos
                .Include(s => s.Pedido)
                    .ThenInclude(p => p.Cliente)
                .Include(s => s.Conductor)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (seguimiento == null)
                return NotFound("No existe el seguimiento");

            bool autorizado = false;
            if (rol == "ADMINISTRADOR")
                autorizado = true;
            else if (rol == "CLIENTE")
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.UsuarioId == userId);
                if (cliente != null && seguimiento.Pedido.ClienteId == cliente.Id)
                    autorizado = true;
            }
            else if (rol == "CONDUCTOR")
            {
                var conductor = await _context.Conductores.FirstOrDefaultAsync(c => c.UsuarioId == userId);
                if (conductor != null && seguimiento.ConductorId == conductor.Id)
                    autorizado = true;
            }

            if (!autorizado)
                return Forbid("No tienes permiso para ver este seguimiento.");

            return Ok(_mapper.Map<SeguimientoDTO>(seguimiento));
        }

        // POST: api/Seguimiento (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<SeguimientoDTO>> Post(SeguimientoCreaDTO dto)
        {
            // Validar existencia de FK
            if (!await _context.Pedidos.AnyAsync(p => p.Id == dto.PedidoId))
                return BadRequest($"El pedido con Id {dto.PedidoId} no existe.");
            if (!await _context.Conductores.AnyAsync(c => c.Id == dto.ConductorId))
                return BadRequest($"El conductor con Id {dto.ConductorId} no existe.");
            if (!await _context.Vehiculos.AnyAsync(v => v.Id == dto.VehiculoId))
                return BadRequest($"El vehículo con Id {dto.VehiculoId} no existe.");
            if (!await _context.Ubicaciones.AnyAsync(u => u.Id == dto.UbicacionId))
                return BadRequest($"La ubicación con Id {dto.UbicacionId} no existe.");

            var seguimiento = _mapper.Map<Seguimiento>(dto);
            _context.Seguimientos.Add(seguimiento);
            await _context.SaveChangesAsync();

            var seguimientoDTO = _mapper.Map<SeguimientoDTO>(seguimiento);
            return CreatedAtRoute("ObtenerSeguimiento", new { id = seguimiento.Id }, seguimientoDTO);
        }

        // PUT: api/Seguimiento/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, SeguimientoCreaDTO dto)
        {
            var seguimiento = await _context.Seguimientos.FindAsync(id);
            if (seguimiento == null)
                return NotFound($"No existe el seguimiento con Id {id}");

            // Validar FK si cambian
            if (seguimiento.PedidoId != dto.PedidoId && !await _context.Pedidos.AnyAsync(p => p.Id == dto.PedidoId))
                return BadRequest($"El pedido con Id {dto.PedidoId} no existe.");
            if (seguimiento.ConductorId != dto.ConductorId && !await _context.Conductores.AnyAsync(c => c.Id == dto.ConductorId))
                return BadRequest($"El conductor con Id {dto.ConductorId} no existe.");
            if (seguimiento.VehiculoId != dto.VehiculoId && !await _context.Vehiculos.AnyAsync(v => v.Id == dto.VehiculoId))
                return BadRequest($"El vehículo con Id {dto.VehiculoId} no existe.");
            if (seguimiento.UbicacionId != dto.UbicacionId && !await _context.Ubicaciones.AnyAsync(u => u.Id == dto.UbicacionId))
                return BadRequest($"La ubicación con Id {dto.UbicacionId} no existe.");

            _mapper.Map(dto, seguimiento);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Seguimiento/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var seguimiento = await _context.Seguimientos.FindAsync(id);
            if (seguimiento == null)
                return NotFound("No existe el seguimiento");

            // Verificar dependencias con HistorialUbicacion
            if (await _context.HistorialUbicaciones.AnyAsync(h => h.SeguimientoId == id))
                return BadRequest("No se puede eliminar el seguimiento porque tiene registros en el historial de ubicaciones.");

            _context.Seguimientos.Remove(seguimiento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}