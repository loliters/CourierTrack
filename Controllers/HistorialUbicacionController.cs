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
    public class HistorialUbicacionController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public HistorialUbicacionController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/HistorialUbicacion (público)
        [HttpGet]
        public async Task<ActionResult<List<HistorialUbicacionDTO>>> Get()
        {
            var historiales = await _context.HistorialUbicaciones.ToListAsync();
            return Ok(_mapper.Map<List<HistorialUbicacionDTO>>(historiales));
        }

        // GET: api/HistorialUbicacion/5 (público)
        [HttpGet("{id:int}", Name = "ObtenerHistorialUbicacion")]
        public async Task<ActionResult<HistorialUbicacionDTO>> Get(int id)
        {
            var historial = await _context.HistorialUbicaciones.FindAsync(id);
            if (historial == null)
                return NotFound("No existe el historial de ubicación");

            return Ok(_mapper.Map<HistorialUbicacionDTO>(historial));
        }

        // POST: api/HistorialUbicacion (solo Administrador)
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<HistorialUbicacionDTO>> Post(HistorialUbicacionCreaDTO historialCreaDTO)
        {
            // Verificar que la Ubicacion exista
            var ubicacionExiste = await _context.Ubicaciones.AnyAsync(u => u.Id == historialCreaDTO.UbicacionId);
            if (!ubicacionExiste)
                return BadRequest($"La ubicación con Id {historialCreaDTO.UbicacionId} no existe.");

            // Verificar que el Seguimiento exista
            var seguimientoExiste = await _context.Seguimientos.AnyAsync(s => s.Id == historialCreaDTO.SeguimientoId);
            if (!seguimientoExiste)
                return BadRequest($"El seguimiento con Id {historialCreaDTO.SeguimientoId} no existe.");

            var historial = _mapper.Map<HistorialUbicacion>(historialCreaDTO);
            _context.HistorialUbicaciones.Add(historial);
            await _context.SaveChangesAsync();

            var historialDTO = _mapper.Map<HistorialUbicacionDTO>(historial);
            return CreatedAtRoute("ObtenerHistorialUbicacion", new { id = historial.Id }, historialDTO);
        }

        // PUT: api/HistorialUbicacion/5 (solo Administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(int id, HistorialUbicacionCreaDTO historialCreaDTO)
        {
            var historial = await _context.HistorialUbicaciones.FindAsync(id);
            if (historial == null)
                return NotFound($"No existe el historial de ubicación con Id {id}");

            // Validar FK si cambian
            if (historial.UbicacionId != historialCreaDTO.UbicacionId)
            {
                var ubicacionExiste = await _context.Ubicaciones.AnyAsync(u => u.Id == historialCreaDTO.UbicacionId);
                if (!ubicacionExiste)
                    return BadRequest($"La ubicación con Id {historialCreaDTO.UbicacionId} no existe.");
            }

            if (historial.SeguimientoId != historialCreaDTO.SeguimientoId)
            {
                var seguimientoExiste = await _context.Seguimientos.AnyAsync(s => s.Id == historialCreaDTO.SeguimientoId);
                if (!seguimientoExiste)
                    return BadRequest($"El seguimiento con Id {historialCreaDTO.SeguimientoId} no existe.");
            }

            _mapper.Map(historialCreaDTO, historial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/HistorialUbicacion/5 (solo Administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var historial = await _context.HistorialUbicaciones.FindAsync(id);
            if (historial == null)
                return NotFound("No existe el historial de ubicación");

            // No hay dependencias posteriores, se puede eliminar directamente
            _context.HistorialUbicaciones.Remove(historial);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
