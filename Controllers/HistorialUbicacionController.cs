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
    public class HistorialUbicacionController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public HistorialUbicacionController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/HistorialUbicacion (cualquier usuario autenticado)
        [HttpGet]
        public async Task<ActionResult<List<HistorialUbicacionDTO>>> Get()
        {
            var historiales = await _context.HistorialUbicaciones.ToListAsync();
            return Ok(_mapper.Map<List<HistorialUbicacionDTO>>(historiales));
        }

        // GET: api/HistorialUbicacion/5
        [HttpGet("{id:int}", Name = "ObtenerHistorialUbicacion")]
        public async Task<ActionResult<HistorialUbicacionDTO>> Get(int id)
        {
            var historial = await _context.HistorialUbicaciones.FindAsync(id);
            if (historial == null)
                return NotFound("No existe el historial de ubicación");

            // Opcional: verificar permisos si se requiere que solo el dueño del pedido pueda verlo
            // Aquí se deja accesible a cualquier autenticado por simplicidad.

            return Ok(_mapper.Map<HistorialUbicacionDTO>(historial));
        }

        // POST: api/HistorialUbicacion (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<HistorialUbicacionDTO>> Post(HistorialUbicacionCreaDTO historialCreaDTO)
        {
            // Verificar que la Ubicacion exista
            if (!await _context.Ubicaciones.AnyAsync(u => u.Id == historialCreaDTO.UbicacionId))
                return BadRequest($"La ubicación con Id {historialCreaDTO.UbicacionId} no existe.");

            // Verificar que el Seguimiento exista
            if (!await _context.Seguimientos.AnyAsync(s => s.Id == historialCreaDTO.SeguimientoId))
                return BadRequest($"El seguimiento con Id {historialCreaDTO.SeguimientoId} no existe.");

            var historial = _mapper.Map<HistorialUbicacion>(historialCreaDTO);
            _context.HistorialUbicaciones.Add(historial);
            await _context.SaveChangesAsync();

            var historialDTO = _mapper.Map<HistorialUbicacionDTO>(historial);
            return CreatedAtRoute("ObtenerHistorialUbicacion", new { id = historial.Id }, historialDTO);
        }

        // PUT: api/HistorialUbicacion/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, HistorialUbicacionCreaDTO historialCreaDTO)
        {
            var historial = await _context.HistorialUbicaciones.FindAsync(id);
            if (historial == null)
                return NotFound($"No existe el historial de ubicación con Id {id}");

            // Validar FK si cambian
            if (historial.UbicacionId != historialCreaDTO.UbicacionId)
            {
                if (!await _context.Ubicaciones.AnyAsync(u => u.Id == historialCreaDTO.UbicacionId))
                    return BadRequest($"La ubicación con Id {historialCreaDTO.UbicacionId} no existe.");
            }

            if (historial.SeguimientoId != historialCreaDTO.SeguimientoId)
            {
                if (!await _context.Seguimientos.AnyAsync(s => s.Id == historialCreaDTO.SeguimientoId))
                    return BadRequest($"El seguimiento con Id {historialCreaDTO.SeguimientoId} no existe.");
            }

            _mapper.Map(historialCreaDTO, historial);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/HistorialUbicacion/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var historial = await _context.HistorialUbicaciones.FindAsync(id);
            if (historial == null)
                return NotFound("No existe el historial de ubicación");

            _context.HistorialUbicaciones.Remove(historial);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}