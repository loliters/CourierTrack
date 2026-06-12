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
    [Authorize] // Todos los endpoints requieren autenticación
    public class MarcaController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public MarcaController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Marca (cualquier usuario autenticado)
        [HttpGet]
        public async Task<ActionResult<List<MarcaDTO>>> Get()
        {
            var marcas = await _context.Marcas.ToListAsync();
            return Ok(_mapper.Map<List<MarcaDTO>>(marcas));
        }

        // GET: api/Marca/5
        [HttpGet("{id:int}", Name = "ObtenerMarca")]
        public async Task<ActionResult<MarcaDTO>> Get(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
                return NotFound("No existe la marca");
            return Ok(_mapper.Map<MarcaDTO>(marca));
        }

        // POST: api/Marca (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<MarcaDTO>> Post(MarcaCreaDTO dto)
        {
            // Verificar duplicado (nombre único)
            if (await _context.Marcas.AnyAsync(m => m.Nombre == dto.Nombre))
                return BadRequest($"Ya existe una marca con el nombre '{dto.Nombre}'.");

            var marca = _mapper.Map<Marca>(dto);
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();

            var marcaDTO = _mapper.Map<MarcaDTO>(marca);
            return CreatedAtRoute("ObtenerMarca", new { id = marca.Id }, marcaDTO);
        }

        // PUT: api/Marca/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, MarcaCreaDTO dto)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
                return NotFound($"No existe la marca con Id {id}");

            // Verificar duplicado excluyendo el propio registro
            if (await _context.Marcas.AnyAsync(m => m.Nombre == dto.Nombre && m.Id != id))
                return Conflict("Ya existe otra marca con ese nombre.");

            _mapper.Map(dto, marca);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Marca/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
                return NotFound("No existe la marca");

            // Verificar si hay modelos que usan esta marca
            if (await _context.Modelos.AnyAsync(m => m.MarcaId == id))
                return BadRequest("No se puede eliminar la marca porque hay modelos asociados.");

            _context.Marcas.Remove(marca);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}