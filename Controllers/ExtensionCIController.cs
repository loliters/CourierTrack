using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCourierTrack.DTO;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtensionCIController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ExtensionCIController(
            ApplicationDBContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<List<ExtensionCIDTO>>> Get()
        {
            var extensionesCI = await _context.ExtensionCI
                .ToListAsync();

            return _mapper.Map<List<ExtensionCIDTO>>(
                extensionesCI);
        }

        // GET
        [HttpGet("{id:int}", Name = "ObtenerExtensionCI")]
        public async Task<ActionResult<ExtensionCIDTO>> Get(int id)
        {
            var extensionCI = await _context.ExtensionCI
                .FirstOrDefaultAsync(x => x.Id == id);

            if (extensionCI == null)
            {
                return NotFound(
                    "Extensión de CI no encontrada.");
            }

            return _mapper.Map<ExtensionCIDTO>(
                extensionCI);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> Post(
            [FromBody] ExtensionCICrearDTO extensionCICrearDTO)
        {
            var existe = await _context.ExtensionCI
                .AnyAsync(x =>
                    x.Nombre == extensionCICrearDTO.Nombre);

            if (existe)
            {
                return BadRequest(
                    $"Ya existe una extensión CI con el nombre {extensionCICrearDTO.Nombre}");
            }

            var extensionCI =
                _mapper.Map<ExtensionCI>(
                    extensionCICrearDTO);

            _context.Add(extensionCI);

            await _context.SaveChangesAsync();

            var extensionCIDTO =
                _mapper.Map<ExtensionCIDTO>(
                    extensionCI);

            return CreatedAtRoute(
                "ObtenerExtensionCI",
                new { id = extensionCI.Id },
                extensionCIDTO);
        }

        // PUT
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(
            int id,
            ExtensionCICrearDTO extensionCICrearDTO)
        {
            var existeExtensionCI =
                await _context.ExtensionCI
                .AnyAsync(x => x.Id == id);

            if (!existeExtensionCI)
            {
                return NotFound(
                    "No existe la extensión CI.");
            }

            var extensionCI =
                _mapper.Map<ExtensionCI>(
                    extensionCICrearDTO);

            extensionCI.Id = id;

            _context.Update(extensionCI);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var extensionCI =
                await _context.ExtensionCI
                .FindAsync(id);

            if (extensionCI == null)
            {
                return NotFound(
                    "La extensión CI no existe.");
            }

            // Verificar si está siendo usada
            var tieneClientes =
                await _context.Clientes
                .AnyAsync(x =>
                    x.ExtensionCIId == id);

            if (tieneClientes)
            {
                return BadRequest(
                    "No se puede eliminar la extensión CI porque tiene clientes asociados.");
            }

            _context.ExtensionCI
                .Remove(extensionCI);

            await _context.SaveChangesAsync();

            return Ok(
                "Extensión CI eliminada correctamente.");
        }
    }
}