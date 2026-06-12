using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCourierTrack.DTO;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoPagoController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public EstadoPagoController(
            ApplicationDBContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<List<EstadoPagoDTO>>> Get()
        {
            var estadosPago = await _context.EstadoPagos
                .ToListAsync();

            return _mapper.Map<List<EstadoPagoDTO>>(
                estadosPago);
        }

        // GET
        [HttpGet("{id:int}", Name = "ObtenerEstadoPago")]
        public async Task<ActionResult<EstadoPagoDTO>> Get(int id)
        {
            var estadoPago = await _context.EstadoPagos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (estadoPago == null)
            {
                return NotFound(
                    "Estado de pago no encontrado.");
            }

            return _mapper.Map<EstadoPagoDTO>(
                estadoPago);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> Post(
            [FromBody] EstadoPagoCreaDTO estadoPagoCreaDTO)
        {
            var existe = await _context.EstadoPagos
                .AnyAsync(x =>
                    x.Nombre == estadoPagoCreaDTO.Nombre);

            if (existe)
            {
                return BadRequest(
                    $"Ya existe un estado de pago con el nombre {estadoPagoCreaDTO.Nombre}");
            }

            var estadoPago =
                _mapper.Map<EstadoPago>(
                    estadoPagoCreaDTO);

            _context.Add(estadoPago);

            await _context.SaveChangesAsync();

            var estadoPagoDTO =
                _mapper.Map<EstadoPagoDTO>(
                    estadoPago);

            return CreatedAtRoute(
                "ObtenerEstadoPago",
                new { id = estadoPago.Id },
                estadoPagoDTO);
        }

        // PUT
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(
            int id,
            EstadoPagoCreaDTO estadoPagoCreaDTO)
        {
            var existeEstadoPago =
                await _context.EstadoPagos
                .AnyAsync(x => x.Id == id);

            if (!existeEstadoPago)
            {
                return NotFound(
                    "No existe el estado de pago.");
            }

            var estadoPago =
                _mapper.Map<EstadoPago>(
                    estadoPagoCreaDTO);

            estadoPago.Id = id;

            _context.Update(estadoPago);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var estadoPago =
                await _context.EstadoPagos
                .FindAsync(id);

            if (estadoPago == null)
            {
                return NotFound(
                    "El estado de pago no existe.");
            }

            _context.EstadoPagos
                .Remove(estadoPago);

            await _context.SaveChangesAsync();

            return Ok(
                "Estado de pago eliminado correctamente.");
        }
    }
}