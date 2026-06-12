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
    public class ConductorController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ConductorController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Conductor 
        [HttpGet]
        public async Task<ActionResult<List<ConductorDTO>>> Get()
        {
            var conductores = await _context.Conductores.ToListAsync();
            return Ok(_mapper.Map<List<ConductorDTO>>(conductores));
        }

        // GET: api/Conductor/5 
        [HttpGet("{id:int}", Name = "ObtenerConductor")]
        public async Task<ActionResult<ConductorDTO>> Get(int id)
        {
            var conductor = await _context.Conductores.FindAsync(id);
            if (conductor == null)
                return NotFound("No existe el conductor");

            return Ok(_mapper.Map<ConductorDTO>(conductor));
        }

        // POST: api/Conductor (solo Administrador)
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<ConductorDTO>> Post(ConductorCreaDTO conductorCreaDTO)
        {
            // Verificar que el UsuarioId exista 
            var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == conductorCreaDTO.UsuarioId);
            if (!usuarioExiste)
                return BadRequest($"El usuario con Id {conductorCreaDTO.UsuarioId} no existe.");

            // Verificar que el TipoLicenciaId exista
            var tipoLicenciaExiste = await _context.TipoLicencias.AnyAsync(tl => tl.Id == conductorCreaDTO.TipoLicenciaId);
            if (!tipoLicenciaExiste)
                return BadRequest($"El tipo de licencia con Id {conductorCreaDTO.TipoLicenciaId} no existe.");

            // Verificar duplicado de número de licencia
            var licenciaDuplicada = await _context.Conductores.AnyAsync(c => c.NroLicencia == conductorCreaDTO.NroLicencia);
            if (licenciaDuplicada)
                return BadRequest($"Ya existe un conductor con la licencia '{conductorCreaDTO.NroLicencia}'.");

            // Verificar que el usuario no sea ya conductor
            var usuarioYaConductor = await _context.Conductores.AnyAsync(c => c.UsuarioId == conductorCreaDTO.UsuarioId);
            if (usuarioYaConductor)
                return BadRequest($"El usuario con Id {conductorCreaDTO.UsuarioId} ya está registrado como conductor.");

            var conductor = _mapper.Map<Conductor>(conductorCreaDTO);
            _context.Conductores.Add(conductor);
            await _context.SaveChangesAsync();

            var conductorDTO = _mapper.Map<ConductorDTO>(conductor);
            return CreatedAtRoute("ObtenerConductor", new { id = conductor.Id }, conductorDTO);
        }

        // PUT: api/Conductor/5 (solo Administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(int id, ConductorCreaDTO conductorCreaDTO)
        {
            var conductor = await _context.Conductores.FindAsync(id);
            if (conductor == null)
                return NotFound($"No existe el conductor con Id {id}");

            // Validar existencia de TipoLicencia
            var tipoLicenciaExiste = await _context.TipoLicencias.AnyAsync(tl => tl.Id == conductorCreaDTO.TipoLicenciaId);
            if (!tipoLicenciaExiste)
                return BadRequest($"El tipo de licencia con Id {conductorCreaDTO.TipoLicenciaId} no existe.");

            // Si se cambia el número de licencia, verificar duplicado
            if (conductor.NroLicencia != conductorCreaDTO.NroLicencia)
            {
                var licenciaDuplicada = await _context.Conductores.AnyAsync(c => c.NroLicencia == conductorCreaDTO.NroLicencia && c.Id != id);
                if (licenciaDuplicada)
                    return BadRequest($"Ya existe otro conductor con la licencia '{conductorCreaDTO.NroLicencia}'.");
            }

            // Si se cambia el UsuarioId, verificar que el nuevo usuario no sea ya conductor
            if (conductor.UsuarioId != conductorCreaDTO.UsuarioId)
            {
                var usuarioYaConductor = await _context.Conductores.AnyAsync(c => c.UsuarioId == conductorCreaDTO.UsuarioId && c.Id != id);
                if (usuarioYaConductor)
                    return BadRequest($"El usuario con Id {conductorCreaDTO.UsuarioId} ya está registrado como otro conductor.");
            }

            _mapper.Map(conductorCreaDTO, conductor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Conductor/5 (solo Administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var conductor = await _context.Conductores.FindAsync(id);
            if (conductor == null)
                return NotFound("No existe el conductor");

            // Verificar dependencias (vehículos asignados, seguimientos)
            var tieneVehiculo = await _context.Vehiculos.AnyAsync(v => v.ConductorId == id);
            if (tieneVehiculo)
                return BadRequest("No se puede eliminar el conductor porque tiene vehículos asignados.");

            var tieneSeguimiento = await _context.Seguimientos.AnyAsync(s => s.ConductorId == id);
            if (tieneSeguimiento)
                return BadRequest("No se puede eliminar el conductor porque tiene seguimientos asociados.");

            _context.Conductores.Remove(conductor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
