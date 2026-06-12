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
    public class ColorController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ColorController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Color (público)
        [HttpGet]
        public async Task<ActionResult<List<ColorDTO>>> Get()
        {
            var colores = await _context.Colores.ToListAsync();
            return Ok(_mapper.Map<List<ColorDTO>>(colores));
        }

        // GET: api/Color/5 (público)
        [HttpGet("{id:int}", Name = "ObtenerColor")]
        public async Task<ActionResult<ColorDTO>> Get(int id)
        {
            var color = await _context.Colores.FindAsync(id);
            if (color == null)
                return NotFound("No existe el color");

            return Ok(_mapper.Map<ColorDTO>(color));
        }

        // POST: api/Color (solo Administrador)
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<ColorDTO>> Post(ColorCreaDTO colorCreaDTO)
        {
            // Verificar duplicado (nombre único)
            var existe = await _context.Colores.AnyAsync(x => x.Nombre == colorCreaDTO.Nombre);
            if (existe)
                return BadRequest($"Ya existe un color con el nombre '{colorCreaDTO.Nombre}'.");

            var color = _mapper.Map<Color>(colorCreaDTO);
            _context.Colores.Add(color);
            await _context.SaveChangesAsync();

            var colorDTO = _mapper.Map<ColorDTO>(color);
            return CreatedAtRoute("ObtenerColor", new { id = color.Id }, colorDTO);
        }

        // PUT: api/Color/5 (solo Administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(int id, ColorCreaDTO colorCreaDTO)
        {
            var color = await _context.Colores.FindAsync(id);
            if (color == null)
                return NotFound($"No existe el color con Id {id}");

            // Verificar duplicado excluyendo el propio registro
            var duplicado = await _context.Colores
                .AnyAsync(x => x.Nombre == colorCreaDTO.Nombre && x.Id != id);
            if (duplicado)
                return Conflict("Ya existe otro color con ese nombre.");

            _mapper.Map(colorCreaDTO, color);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Color/5 (solo Administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var color = await _context.Colores.FindAsync(id);
            if (color == null)
                return NotFound("No existe el color");

            // Verificar si hay vehículos asociados
            var tieneVehiculo = await _context.Vehiculos.AnyAsync(v => v.ColorId == id);
            if (tieneVehiculo)
                return BadRequest("No se puede eliminar el color porque hay vehículos asociados.");

            _context.Colores.Remove(color);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
