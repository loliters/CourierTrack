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
    public class GeneroController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public GeneroController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Genero (cualquier usuario autenticado)
        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            var generos = await _context.Generos.ToListAsync();
            return Ok(_mapper.Map<List<GeneroDTO>>(generos));
        }

        // GET: api/Genero/5
        [HttpGet("{id:int}", Name = "ObtenerGenero")]
        public async Task<ActionResult<GeneroDTO>> Get(int id)
        {
            var genero = await _context.Generos.FindAsync(id);
            if (genero == null)
                return NotFound("Género no encontrado.");
            return Ok(_mapper.Map<GeneroDTO>(genero));
        }

        // POST: api/Genero (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<GeneroDTO>> Post(GeneroCreaDTO dto)
        {
            if (await _context.Generos.AnyAsync(g => g.Nombre == dto.Nombre))
                return BadRequest($"Ya existe un género con el nombre '{dto.Nombre}'.");

            var genero = _mapper.Map<Genero>(dto);
            _context.Generos.Add(genero);
            await _context.SaveChangesAsync();

            var generoDTO = _mapper.Map<GeneroDTO>(genero);
            return CreatedAtRoute("ObtenerGenero", new { id = genero.Id }, generoDTO);
        }

        // PUT: api/Genero/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, GeneroCreaDTO dto)
        {
            var genero = await _context.Generos.FindAsync(id);
            if (genero == null)
                return NotFound("No existe el género.");

            // Verificar duplicado excluyendo el propio registro
            if (await _context.Generos.AnyAsync(g => g.Nombre == dto.Nombre && g.Id != id))
                return Conflict("Ya existe otro género con ese nombre.");

            _mapper.Map(dto, genero);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Genero/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var genero = await _context.Generos.FindAsync(id);
            if (genero == null)
                return NotFound("El género no existe.");

            // Verificar si tiene clientes naturales asociados
            var tieneClientes = await _context.ClientesNatural.AnyAsync(cn => cn.GeneroId == id);
            if (tieneClientes)
                return BadRequest("No se puede eliminar el género porque tiene clientes asociados.");

            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();
            return Ok("Género eliminado correctamente.");
        }
    }
}