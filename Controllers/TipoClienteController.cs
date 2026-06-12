using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCourierTrack.DTO;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoClienteController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public TipoClienteController(
            ApplicationDBContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<List<TipoClienteDTO>>> Get()
        {
            var tiposCliente = await _context.TipoClientes
                .ToListAsync();

            return _mapper.Map<List<TipoClienteDTO>>(tiposCliente);
        }

        // GET
        [HttpGet("{id:int}", Name = "ObtenerTipoCliente")]
        public async Task<ActionResult<TipoClienteDTO>> Get(int id)
        {
            var tipoCliente = await _context.TipoClientes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (tipoCliente == null)
                return NotFound("Tipo de cliente no encontrado.");

            return _mapper.Map<TipoClienteDTO>(tipoCliente);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> Post(
            [FromBody] TipoClienteCreaDTO tipoClienteCreaDTO)
        {
            var existe = await _context.TipoClientes
                .AnyAsync(x => x.Nombre == tipoClienteCreaDTO.Nombre);

            if (existe)
            {
                return BadRequest(
                    $"Ya existe un tipo de cliente con el nombre {tipoClienteCreaDTO.Nombre}");
            }

            var tipoCliente = _mapper.Map<TipoCliente>(
                tipoClienteCreaDTO);

            _context.Add(tipoCliente);

            await _context.SaveChangesAsync();

            var tipoClienteDTO = _mapper.Map<TipoClienteDTO>(
                tipoCliente);

            return CreatedAtRoute(
                "ObtenerTipoCliente",
                new { id = tipoCliente.Id },
                tipoClienteDTO);
        }

        // PUT
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(
            int id,
            TipoClienteCreaDTO tipoClienteCreaDTO)
        {
            var existeTipoCliente = await _context.TipoClientes
                .AnyAsync(x => x.Id == id);

            if (!existeTipoCliente)
                return NotFound(
                    "No existe el tipo de cliente.");

            var tipoCliente = _mapper.Map<TipoCliente>(
                tipoClienteCreaDTO);

            tipoCliente.Id = id;

            _context.Update(tipoCliente);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tipoCliente = await _context.TipoClientes
                .FindAsync(id);

            if (tipoCliente == null)
                return NotFound(
                    "El tipo de cliente no existe.");

            // Verificar si está siendo usado
            var tieneClientes = await _context.Clientes
                .AnyAsync(x => x.TipoClienteId == id);

            if (tieneClientes)
            {
                return BadRequest(
                    "No se puede eliminar el tipo de cliente porque tiene clientes asociados.");
            }

            _context.TipoClientes.Remove(tipoCliente);

            await _context.SaveChangesAsync();

            return Ok(
                "Tipo de cliente eliminado correctamente.");
        }
    }
}