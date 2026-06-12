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
    public class ExtensionCIController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ExtensionCIController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ExtensionCI (cualquier usuario autenticado)
        [HttpGet]
        public async Task<ActionResult<List<ExtensionCIDTO>>> Get()
        {
            var extensiones = await _context.ExtensionCI.ToListAsync();
            return Ok(_mapper.Map<List<ExtensionCIDTO>>(extensiones));
        }

        // GET: api/ExtensionCI/5
        [HttpGet("{id:int}", Name = "ObtenerExtensionCI")]
        public async Task<ActionResult<ExtensionCIDTO>> Get(int id)
        {
            var extension = await _context.ExtensionCI.FindAsync(id);
            if (extension == null)
                return NotFound("Extensión de CI no encontrada.");
            return Ok(_mapper.Map<ExtensionCIDTO>(extension));
        }

        // POST: api/ExtensionCI (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<ExtensionCIDTO>> Post(ExtensionCICrearDTO dto)
        {
            // Verificar duplicado (nombre único)
            if (await _context.ExtensionCI.AnyAsync(e => e.Nombre == dto.Nombre))
                return BadRequest($"Ya existe una extensión CI con el nombre '{dto.Nombre}'.");

            var extension = _mapper.Map<ExtensionCI>(dto);
            _context.ExtensionCI.Add(extension);
            await _context.SaveChangesAsync();

            var extensionDTO = _mapper.Map<ExtensionCIDTO>(extension);
            return CreatedAtRoute("ObtenerExtensionCI", new { id = extension.Id }, extensionDTO);
        }

        // PUT: api/ExtensionCI/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, ExtensionCICrearDTO dto)
        {
            var extension = await _context.ExtensionCI.FindAsync(id);
            if (extension == null)
                return NotFound("No existe la extensión CI.");

            // Verificar duplicado excluyendo el propio registro
            if (await _context.ExtensionCI.AnyAsync(e => e.Nombre == dto.Nombre && e.Id != id))
                return Conflict("Ya existe otra extensión CI con ese nombre.");

            _mapper.Map(dto, extension);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/ExtensionCI/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var extension = await _context.ExtensionCI.FindAsync(id);
            if (extension == null)
                return NotFound("La extensión CI no existe.");

            // Verificar si tiene clientes asociados
            var tieneClientes = await _context.Clientes.AnyAsync(c => c.ExtensionCIId == id);
            if (tieneClientes)
                return BadRequest("No se puede eliminar la extensión CI porque tiene clientes asociados.");

            _context.ExtensionCI.Remove(extension);
            await _context.SaveChangesAsync();
            return Ok("Extensión CI eliminada correctamente.");
        }
    }
}