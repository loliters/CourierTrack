using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCourierTrack.DTO;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public GeneroController(
            ApplicationDBContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            var generos = await _context.Generos.ToListAsync();

            return _mapper.Map<List<GeneroDTO>>(generos);
        }

        // GET
        [HttpGet("{id:int}", Name = "ObtenerGenero")]
        public async Task<ActionResult<GeneroDTO>> Get(int id)
        {
            var genero = await _context.Generos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (genero == null)
                return NotFound("Género no encontrado.");

            return _mapper.Map<GeneroDTO>(genero);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> Post(
            [FromBody] GeneroCreaDTO generoCreaDTO)
        {
            var existe = await _context.Generos
                .AnyAsync(x => x.Nombre == generoCreaDTO.Nombre);

            if (existe)
            {
                return BadRequest(
                    $"Ya existe un género con el nombre {generoCreaDTO.Nombre}");
            }

            var genero = _mapper.Map<Genero>(generoCreaDTO);

            _context.Add(genero);
            await _context.SaveChangesAsync();

            var generoDTO = _mapper.Map<GeneroDTO>(genero);

            return CreatedAtRoute(
                "ObtenerGenero",
                new { id = genero.Id },
                generoDTO);
        }

        // PUT
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(
            int id,
            GeneroCreaDTO generoCreaDTO)
        {
            var existeGenero = await _context.Generos
                .AnyAsync(x => x.Id == id);

            if (!existeGenero)
                return NotFound("No existe el género.");

            var genero = _mapper.Map<Genero>(generoCreaDTO);
            genero.Id = id;

            _context.Update(genero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genero = await _context.Generos
                .FindAsync(id);

            if (genero == null)
                return NotFound("El género no existe.");

            // Verificar si el género está siendo usado
            var tieneClientes = await _context.ClientesNatural
                .AnyAsync(x => x.GeneroId == id);

            if (tieneClientes)
            {
                return BadRequest(
                    "No se puede eliminar el género porque tiene clientes asociados.");
            }

            _context.Generos.Remove(genero);

            await _context.SaveChangesAsync();

            return Ok("Género eliminado correctamente.");
        }
    }
}