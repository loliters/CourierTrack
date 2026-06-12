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
    public class MarcaController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public MarcaController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Marca
        [HttpGet]
        public async Task<ActionResult<List<MarcaDTO>>> Get()
        {
            var marcas = await _context.Marcas.ToListAsync();
            return _mapper.Map<List<MarcaDTO>>(marcas);
        }

        // GET: api/marca/5
        [HttpGet("{id:int}", Name = "ObtenerMarca")]
        public async Task<ActionResult<MarcaDTO>> Get(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
                return NotFound("No existe la marca");

            return _mapper.Map<MarcaDTO>(marca);
        }

        // POST: api/Marca//solo Administrador
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<MarcaDTO>> Post(MarcaCreaDTO marcaCreaDTO)
        {
            // Verificar duplicado (ya existe una marca igual)
            var existe = await _context.Marcas.AnyAsync(x => x.Nombre == marcaCreaDTO.Nombre);
            if (existe)
            {
                return BadRequest($"Ya existe una marca con ese nombre {marcaCreaDTO.Nombre}");
            }

            var marcas = _mapper.Map<Marca>(marcaCreaDTO);
            _context.Marcas.Add(marcas);
            await _context.SaveChangesAsync();

            var marcaDTO = _mapper.Map<MarcaDTO>(marcas);
            return CreatedAtRoute("ObtenerMarca", new { id = marcas.Id }, marcaDTO);
        }

        // PUT//solo Administrador
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(int id, MarcaCreaDTO marcaCreaDTO)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca== null)
                return NotFound($"No existe la marca con Id {id}");

            // Verificar duplicado excluyendo el propio registro
            var duplicado = await _context.Marcas
                .AnyAsync(x => x.Nombre == marcaCreaDTO.Nombre && x.Id != id);
            if (duplicado)
                return Conflict("Ya existe otro marca con ese nombre.");

            _mapper.Map(marcaCreaDTO, marca); // Actualiza la marca
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE//solo Administrador
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca  == null)
                return NotFound("No existe la marca");

            // Verificar si hay modelos que usan esta marca
            var tieneModelo = await _context.Modelos.AnyAsync(m => m.MarcaId == id);
            if (tieneModelo)
                return BadRequest("No se puede eliminar la marca porque hay modelos asociados.");

            _context.Marcas.Remove(marca);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
