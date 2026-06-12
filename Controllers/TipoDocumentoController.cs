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
    public class TipoDocumentoController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public TipoDocumentoController(
            ApplicationDBContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<List<TipoDocumentoDTO>>> Get()
        {
            var tiposDocumento = await _context.TipoDocumentos
                .ToListAsync();

            return _mapper.Map<List<TipoDocumentoDTO>>(
                tiposDocumento);
        }

        // GET
        [HttpGet("{id:int}", Name = "ObtenerTipoDocumento")]
        public async Task<ActionResult<TipoDocumentoDTO>> Get(int id)
        {
            var tipoDocumento = await _context.TipoDocumentos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (tipoDocumento == null)
                return NotFound(
                    "Tipo de documento no encontrado.");

            return _mapper.Map<TipoDocumentoDTO>(
                tipoDocumento);
        }

        // POST
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Post(
            [FromBody] TipoDocumentoCreaDTO tipoDocumentoCreaDTO)
        {
            var existe = await _context.TipoDocumentos
                .AnyAsync(x =>
                    x.Nombre == tipoDocumentoCreaDTO.Nombre);

            if (existe)
            {
                return BadRequest(
                    $"Ya existe un tipo de documento con el nombre {tipoDocumentoCreaDTO.Nombre}");
            }

            var tipoDocumento = _mapper.Map<TipoDocumento>(
                tipoDocumentoCreaDTO);

            _context.Add(tipoDocumento);

            await _context.SaveChangesAsync();

            var tipoDocumentoDTO =
                _mapper.Map<TipoDocumentoDTO>(
                    tipoDocumento);

            return CreatedAtRoute(
                "ObtenerTipoDocumento",
                new { id = tipoDocumento.Id },
                tipoDocumentoDTO);
        }

        // PUT
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Put(
            int id,
            TipoDocumentoCreaDTO tipoDocumentoCreaDTO)
        {
            var existeTipoDocumento =
                await _context.TipoDocumentos
                .AnyAsync(x => x.Id == id);

            if (!existeTipoDocumento)
            {
                return NotFound(
                    "No existe el tipo de documento.");
            }

            var tipoDocumento =
                _mapper.Map<TipoDocumento>(
                    tipoDocumentoCreaDTO);

            tipoDocumento.Id = id;

            _context.Update(tipoDocumento);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var tipoDocumento =
                await _context.TipoDocumentos
                .FindAsync(id);

            if (tipoDocumento == null)
            {
                return NotFound(
                    "El tipo de documento no existe.");
            }

            // Verificar si está siendo usado
            var tieneClientes =
                await _context.Clientes
                .AnyAsync(x =>
                    x.TipoDocumentoId == id);

            if (tieneClientes)
            {
                return BadRequest(
                    "No se puede eliminar el tipo de documento porque tiene clientes asociados.");
            }

            _context.TipoDocumentos
                .Remove(tipoDocumento);

            await _context.SaveChangesAsync();

            return Ok(
                "Tipo de documento eliminado correctamente.");
        }
    }
}