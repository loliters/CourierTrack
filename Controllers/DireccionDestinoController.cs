using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCourierTrack.DTO;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionDestinoController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public DireccionDestinoController(
            ApplicationDBContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<
            List<DireccionDestinoDTO>>> Get()
        {
            var direcciones =
                await _context.DireccionesDestinos
                .ToListAsync();

            return _mapper.Map<
                List<DireccionDestinoDTO>>(
                    direcciones);
        }

        // GET
        [HttpGet("{id:int}",
            Name = "ObtenerDireccionDestino")]
        public async Task<ActionResult<
            DireccionDestinoDTO>> Get(int id)
        {
            var direccion =
                await _context.DireccionesDestinos
                .FirstOrDefaultAsync(x =>
                    x.Id == id);

            if (direccion == null)
            {
                return NotFound(
                    "Dirección destino no encontrada.");
            }

            return _mapper.Map<
                DireccionDestinoDTO>(
                    direccion);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> Post(
            [FromBody]
            DireccionDestinoCreaDTO
            direccionDestinoCreaDTO)
        {
            // validar ubicación
            var existeUbicacion =
                await _context.Ubicaciones
                .AnyAsync(x =>
                    x.Id ==
                    direccionDestinoCreaDTO
                    .UbicacionId);

            if (!existeUbicacion)
            {
                return BadRequest(
                    "La ubicación no existe.");
            }

            var direccion =
                _mapper.Map<
                    DireccionDestino>(
                    direccionDestinoCreaDTO);

            _context.Add(direccion);

            await _context.SaveChangesAsync();

            var direccionDTO =
                _mapper.Map<
                    DireccionDestinoDTO>(
                    direccion);

            return CreatedAtRoute(
                "ObtenerDireccionDestino",
                new
                {
                    id = direccion.Id
                },
                direccionDTO);
        }

        // PUT
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(
            int id,
            DireccionDestinoCreaDTO
            direccionDestinoCreaDTO)
        {
            var existeDireccion =
                await _context
                .DireccionesDestinos
                .AnyAsync(x =>
                    x.Id == id);

            if (!existeDireccion)
            {
                return NotFound("La dirección destino no existe.");
            }

            // validar ubicación
            var existeUbicacion =
                await _context.Ubicaciones
                .AnyAsync(x =>
                    x.Id ==
                    direccionDestinoCreaDTO
                    .UbicacionId);

            if (!existeUbicacion)
            {
                return BadRequest("La ubicación no existe.");
            }

            var direccion =
                _mapper.Map<
                    DireccionDestino>(
                    direccionDestinoCreaDTO);

            direccion.Id = id;

            _context.Update(
                direccion);

            await _context
                .SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult>
            Delete(int id)
        {
            var direccion =
                await _context
                .DireccionesDestinos
                .FindAsync(id);

            if (direccion == null)
            {
                return NotFound("La dirección destino no existe.");
            }

            // validar relación con detalle pedido
            var tieneDetalles =
                await _context
                .DetallePedidos
                .AnyAsync(x =>
                    x.DireccionDestinoId
                    == id);

            if (tieneDetalles)
            {
                return BadRequest( "No se puede eliminar porque tiene detalles de pedido asociados.");
            }

            _context.DireccionesDestinos
                .Remove(direccion);

            await _context
                .SaveChangesAsync();

            return Ok("Dirección destino eliminada correctamente.");
        }
    }
}