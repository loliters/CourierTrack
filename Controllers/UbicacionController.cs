using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCourierTrack.DTO;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public UbicacionController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Ubicacion (público - cualquier usuario autenticado puede ver ubicaciones)
        [HttpGet]
        public async Task<ActionResult<List<UbicacionDTO>>> Get()
        {
            return Ok(_mapper.Map<List<UbicacionDTO>>(await _context.Ubicaciones.ToListAsync()));
        }

        // GET: api/Ubicacion/5 (público)
        [HttpGet("{id:int}", Name = "ObtenerUbicacion")]
        public async Task<ActionResult<UbicacionConUsuariosDTO>> Get(int id)
        {
            var ubicacion = await _context.Ubicaciones
                .Include(u => u.UsuariosUbicaciones)
                    .ThenInclude(uu => uu.Usuario)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (ubicacion == null) return NotFound();
            return Ok(_mapper.Map<UbicacionConUsuariosDTO>(ubicacion));
        }

        // POST: api/Ubicacion (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]  // ← corregido a mayúsculas
        public async Task<ActionResult<UbicacionDTO>> Post(UbicacionCreaDTO dto)
        {
            if (await _context.Ubicaciones.AnyAsync(u => u.Latitud == dto.Latitud && u.Longitud == dto.Longitud))
                return BadRequest("Coordenadas ya existen.");
            var ubicacion = _mapper.Map<Ubicacion>(dto);
            _context.Ubicaciones.Add(ubicacion);
            await _context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerUbicacion", new { id = ubicacion.Id }, _mapper.Map<UbicacionDTO>(ubicacion));
        }

        // PUT: api/Ubicacion/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]  // ← corregido a mayúsculas
        public async Task<IActionResult> Put(int id, UbicacionCreaDTO dto)
        {
            var ubicacion = await _context.Ubicaciones.FindAsync(id);
            if (ubicacion == null) return NotFound();
            if (await _context.Ubicaciones.AnyAsync(u => u.Latitud == dto.Latitud && u.Longitud == dto.Longitud && u.Id != id))
                return Conflict("Coordenadas ya en uso.");
            _mapper.Map(dto, ubicacion);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Ubicacion/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]  // ← corregido a mayúsculas
        public async Task<IActionResult> Delete(int id)
        {
            var ubicacion = await _context.Ubicaciones.FindAsync(id);
            if (ubicacion == null) return NotFound();
            bool dependencias = await _context.DireccionesOrigenes.AnyAsync(d => d.UbicacionId == id) ||
                                await _context.DireccionesDestinos.AnyAsync(d => d.UbicacionId == id) ||
                                await _context.UsuariosUbicaciones.AnyAsync(uu => uu.UbicacionId == id);
            if (dependencias) return BadRequest("Ubicación en uso, no se puede eliminar.");
            _context.Ubicaciones.Remove(ubicacion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}