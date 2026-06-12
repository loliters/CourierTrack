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
    public class EstadoPagoController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public EstadoPagoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/EstadoPago (cualquier usuario autenticado)
        [HttpGet]
        public async Task<ActionResult<List<EstadoPagoDTO>>> Get()
        {
            var estadosPago = await _context.EstadoPagos.ToListAsync();
            return Ok(_mapper.Map<List<EstadoPagoDTO>>(estadosPago));
        }

        // GET: api/EstadoPago/5
        [HttpGet("{id:int}", Name = "ObtenerEstadoPago")]
        public async Task<ActionResult<EstadoPagoDTO>> Get(int id)
        {
            var estadoPago = await _context.EstadoPagos.FindAsync(id);
            if (estadoPago == null)
                return NotFound("Estado de pago no encontrado.");
            return Ok(_mapper.Map<EstadoPagoDTO>(estadoPago));
        }

        // POST: api/EstadoPago (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<EstadoPagoDTO>> Post(EstadoPagoCreaDTO dto)
        {
            // Verificar duplicado (nombre único)
            if (await _context.EstadoPagos.AnyAsync(ep => ep.Nombre == dto.Nombre))
                return BadRequest($"Ya existe un estado de pago con el nombre '{dto.Nombre}'.");

            var estadoPago = _mapper.Map<EstadoPago>(dto);
            _context.EstadoPagos.Add(estadoPago);
            await _context.SaveChangesAsync();

            var estadoPagoDTO = _mapper.Map<EstadoPagoDTO>(estadoPago);
            return CreatedAtRoute("ObtenerEstadoPago", new { id = estadoPago.Id }, estadoPagoDTO);
        }

        // PUT: api/EstadoPago/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, EstadoPagoCreaDTO dto)
        {
            var estadoPago = await _context.EstadoPagos.FindAsync(id);
            if (estadoPago == null)
                return NotFound("No existe el estado de pago.");

            // Verificar duplicado excluyendo el propio registro
            if (await _context.EstadoPagos.AnyAsync(ep => ep.Nombre == dto.Nombre && ep.Id != id))
                return Conflict("Ya existe otro estado de pago con ese nombre.");

            _mapper.Map(dto, estadoPago);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/EstadoPago/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var estadoPago = await _context.EstadoPagos.FindAsync(id);
            if (estadoPago == null)
                return NotFound("El estado de pago no existe.");

            // Verificar si hay pagos asociados
            var tienePagos = await _context.Pagos.AnyAsync(p => p.EstadoPagoId == id);
            if (tienePagos)
                return BadRequest("No se puede eliminar el estado de pago porque tiene pagos asociados.");

            _context.EstadoPagos.Remove(estadoPago);
            await _context.SaveChangesAsync();
            return Ok("Estado de pago eliminado correctamente.");
        }
    }
}