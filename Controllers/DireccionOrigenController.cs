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
    public class DireccionOrigenController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public DireccionOrigenController(
            ApplicationDBContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<
            List<DireccionOrigenDTO>>> Get()
        {
            var direcciones =
                await _context.DireccionesOrigenes
                .ToListAsync();

            return _mapper.Map<
                List<DireccionOrigenDTO>>(
                    direcciones);
        }

        // GET
        [HttpGet("{id:int}",
            Name = "ObtenerDireccionOrigen")]
        public async Task<ActionResult<
            DireccionOrigenDTO>> Get(int id)
        {
            var direccion =
                await _context.DireccionesOrigenes
                .FirstOrDefaultAsync(x =>
                    x.Id == id);

            if (direccion == null)
            {
                return NotFound("Dirección origen no encontrada.");
            }

            return _mapper.Map<
                DireccionOrigenDTO>(
                    direccion);
        }

        // POST
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Post(
            [FromBody]
            DireccionOrigenCreaDTO
            direccionOrigenCreaDTO)
        {
            // validar ubicación
            var existeUbicacion =
                await _context.Ubicaciones
                .AnyAsync(x =>
                    x.Id ==
                    direccionOrigenCreaDTO
                    .UbicacionId);

            if (!existeUbicacion)
            {
                return BadRequest(
                    "La ubicación no existe.");
            }

            var direccion =
                _mapper.Map<
                    DireccionOrigen>(
                    direccionOrigenCreaDTO);

            _context.Add(direccion);

            await _context.SaveChangesAsync();

            var direccionDTO =
                _mapper.Map<
                    DireccionOrigenDTO>(
                    direccion);

            return CreatedAtRoute("ObtenerDireccionOrigen",
                new
                {
                    id = direccion.Id
                },
                direccionDTO);
        }

        // PUT
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Put(
            int id,
            DireccionOrigenCreaDTO
            direccionOrigenCreaDTO)
        {
            var existeDireccion =
                await _context
                .DireccionesOrigenes
                .AnyAsync(x =>
                    x.Id == id);

            if (!existeDireccion)
            {
                return NotFound(
                    "La dirección origen no existe.");
            }

            // validar ubicación
            var existeUbicacion =
                await _context.Ubicaciones
                .AnyAsync(x =>
                    x.Id ==
                    direccionOrigenCreaDTO
                    .UbicacionId);

            if (!existeUbicacion)
            {
                return BadRequest(
                    "La ubicación no existe.");
            }

            var direccion =
                _mapper.Map<
                    DireccionOrigen>(
                    direccionOrigenCreaDTO);

            direccion.Id = id;

            _context.Update(
                direccion);

            await _context
                .SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/direccionorigen/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult>
            Delete(int id)
        {
            var direccion =
                await _context
                .DireccionesOrigenes
                .FindAsync(id);

            if (direccion == null)
            {
                return NotFound(
                    "La dirección origen no existe.");
            }

            // validar relación con detalle pedido
            var tieneDetalles =
                await _context
                .DetallePedidos
                .AnyAsync(x =>
                    x.DireccionOrigenId
                    == id);

            if (tieneDetalles)
            {
                return BadRequest(
                    "No se puede eliminar porque tiene detalles de pedido asociados.");
            }

            _context.DireccionesOrigenes
                .Remove(direccion);

            await _context
                .SaveChangesAsync();

            return Ok(
                "Dirección origen eliminada correctamente.");
        }
    }
}