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
    [Authorize]
    public class CalificacionController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public CalificacionController(
            ApplicationDBContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Calificacion
        [HttpGet]
        public async Task<ActionResult<List<CalificacionDTO>>> Get()
        {
            var calificaciones = await _context.Calificacions.ToListAsync();

            return Ok(_mapper.Map<List<CalificacionDTO>>(calificaciones));
        }

        // GET: api/Calificacion/5
        [HttpGet("{id:int}", Name = "ObtenerCalificacion")]
        public async Task<ActionResult<CalificacionDTO>> Get(int id)
        {
            var calificacion = await _context.Calificacions
                .FirstOrDefaultAsync(x => x.Id == id);

            if (calificacion == null)
                return NotFound("La calificación no existe.");

            return Ok(_mapper.Map<CalificacionDTO>(calificacion));
        }

        // POST: api/Calificacion
        [HttpPost]
        public async Task<ActionResult<CalificacionDTO>> Post(
            CalificacionCreaDTO dto)
        {
            var existePedido = await _context.Pedidos
                .AnyAsync(p => p.Id == dto.PedidoId);

            if (!existePedido)
                return BadRequest("El pedido no existe.");

            var existeCliente = await _context.Clientes
                .AnyAsync(c => c.Id == dto.ClienteId);

            if (!existeCliente)
                return BadRequest("El cliente no existe.");

            var existeConductor = await _context.Conductores
                .AnyAsync(c => c.Id == dto.ConductorId);

            if (!existeConductor)
                return BadRequest("El conductor no existe.");

            var yaExisteCalificacion = await _context.Calificacions
                .AnyAsync(c => c.PedidoId == dto.PedidoId);

            if (yaExisteCalificacion)
                return BadRequest("Este pedido ya fue calificado.");

            var calificacion = _mapper.Map<Calificacion>(dto);

            _context.Calificacions.Add(calificacion);

            await _context.SaveChangesAsync();

            var resultado = _mapper.Map<CalificacionDTO>(calificacion);

            return CreatedAtRoute(
                "ObtenerCalificacion",
                new { id = calificacion.Id },
                resultado);
        }

        // PUT: api/Calificacion/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(
            int id,
            CalificacionCreaDTO dto)
        {
            var calificacion = await _context.Calificacions
                .FirstOrDefaultAsync(c => c.Id == id);

            if (calificacion == null)
                return NotFound("La calificación no existe.");

            var existePedido = await _context.Pedidos
                .AnyAsync(p => p.Id == dto.PedidoId);

            if (!existePedido)
                return BadRequest("El pedido no existe.");

            var existeCliente = await _context.Clientes
                .AnyAsync(c => c.Id == dto.ClienteId);

            if (!existeCliente)
                return BadRequest("El cliente no existe.");

            var existeConductor = await _context.Conductores
                .AnyAsync(c => c.Id == dto.ConductorId);

            if (!existeConductor)
                return BadRequest("El conductor no existe.");

            _mapper.Map(dto, calificacion);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Calificacion/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var calificacion = await _context.Calificacions
                .FirstOrDefaultAsync(c => c.Id == id);

            if (calificacion == null)
                return NotFound("La calificación no existe.");

            _context.Calificacions.Remove(calificacion);

            await _context.SaveChangesAsync();

            return Ok("Calificación eliminada correctamente.");
        }
    }
}