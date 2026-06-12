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
    public class ConductorController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ConductorController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Conductor (solo administrador)
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<List<ConductorDTO>>> Get()
        {
            var conductores = await _context.Conductores
                .Include(c => c.TipoLicencia)
                .Include(c => c.Usuario)
                .ToListAsync();
            return Ok(_mapper.Map<List<ConductorDTO>>(conductores));
        }

        // GET: api/Conductor/5
        [HttpGet("{id:int}", Name = "ObtenerConductor")]
        public async Task<ActionResult<ConductorDTO>> Get(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            var conductor = await _context.Conductores
                .Include(c => c.TipoLicencia)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (conductor == null)
                return NotFound("No existe el conductor");

            // Administrador o el propio conductor
            if (rol != "ADMINISTRADOR" && conductor.UsuarioId != userId)
                return Forbid("No tienes permiso para ver este conductor.");

            return Ok(_mapper.Map<ConductorDTO>(conductor));
        }

        // POST: api/Conductor (permite auto-registro)
        [HttpPost]
        public async Task<ActionResult<ConductorDTO>> Post(ConductorCreaDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimsIdentity.DefaultRoleClaimType)?.Value;

            // Validar permisos: solo administrador o el propio usuario
            if (rol != "ADMINISTRADOR" && dto.UsuarioId != userId)
                return Forbid("No puedes registrar como conductor a otro usuario.");

            // Verificar que el usuario exista
            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
            if (usuario == null)
                return BadRequest($"El usuario con Id {dto.UsuarioId} no existe.");

            // Verificar que el usuario no sea ya conductor
            if (await _context.Conductores.AnyAsync(c => c.UsuarioId == dto.UsuarioId))
                return BadRequest($"El usuario ya está registrado como conductor.");

            // Verificar tipo de licencia
            if (!await _context.TipoLicencias.AnyAsync(tl => tl.Id == dto.TipoLicenciaId))
                return BadRequest($"El tipo de licencia con Id {dto.TipoLicenciaId} no existe.");

            // Verificar licencia duplicada
            if (await _context.Conductores.AnyAsync(c => c.NroLicencia == dto.NroLicencia))
                return BadRequest($"Ya existe un conductor con la licencia '{dto.NroLicencia}'.");

            var conductor = _mapper.Map<Conductor>(dto);
            _context.Conductores.Add(conductor);
            await _context.SaveChangesAsync();

            // Opcional: actualizar el rol del usuario a CONDUCTOR si aún no lo es
            if (usuario.Rol.Nombre != "CONDUCTOR")
            {
                var rolConductor = await _context.Roles.FirstOrDefaultAsync(r => r.Nombre == "CONDUCTOR");
                if (rolConductor != null)
                    usuario.RolId = rolConductor.Id;
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
            }

            var conductorDTO = _mapper.Map<ConductorDTO>(conductor);
            return CreatedAtRoute("ObtenerConductor", new { id = conductor.Id }, conductorDTO);
        }

        // PUT: api/Conductor/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, ConductorCreaDTO dto)
        {
            var conductor = await _context.Conductores.FindAsync(id);
            if (conductor == null)
                return NotFound($"No existe el conductor con Id {id}");

            if (!await _context.TipoLicencias.AnyAsync(tl => tl.Id == dto.TipoLicenciaId))
                return BadRequest($"El tipo de licencia con Id {dto.TipoLicenciaId} no existe.");

            // Verificar duplicados excluyendo el actual
            if (conductor.NroLicencia != dto.NroLicencia &&
                await _context.Conductores.AnyAsync(c => c.NroLicencia == dto.NroLicencia && c.Id != id))
                return BadRequest($"Ya existe otro conductor con la licencia '{dto.NroLicencia}'.");

            // Verificar que el UsuarioId no esté ya en otro conductor
            if (conductor.UsuarioId != dto.UsuarioId &&
                await _context.Conductores.AnyAsync(c => c.UsuarioId == dto.UsuarioId && c.Id != id))
                return BadRequest($"El usuario ya está registrado como otro conductor.");

            _mapper.Map(dto, conductor);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Conductor/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var conductor = await _context.Conductores.FindAsync(id);
            if (conductor == null)
                return NotFound("No existe el conductor");

            // Verificar dependencias
            if (await _context.Vehiculos.AnyAsync(v => v.ConductorId == id))
                return BadRequest("No se puede eliminar el conductor porque tiene vehículos asignados.");

            if (await _context.Seguimientos.AnyAsync(s => s.ConductorId == id))
                return BadRequest("No se puede eliminar el conductor porque tiene seguimientos asociados.");

            _context.Conductores.Remove(conductor);
            await _context.SaveChangesAsync();
            return Ok("Conductor eliminado correctamente.");
        }
    }
}