using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCourierTrack.DTO;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        // GET
        [HttpGet]
        public async Task<ActionResult<
            List<CalificacionDTO>>> Get()
        {
            var calificaciones =
                await _context.Calificacions
                .ToListAsync();

            return _mapper.Map<
                List<CalificacionDTO>>(
                    calificaciones);
        }

        // GET: api/calificacion/5
        [HttpGet("{id:int}",
            Name = "ObtenerCalificacion")]
        public async Task<ActionResult<
            CalificacionDTO>> Get(int id)
        {
            var calificacion =
                await _context.Calificacions
                .FirstOrDefaultAsync(x =>
                    x.Id == id);

            if (calificacion == null)
            {
                return NotFound(
                    "Calificación no encontrada.");
            }

            return _mapper.Map<
                CalificacionDTO>(
                    calificacion);
        }

        // POST: api/calificacion
        [HttpPost]
        public async Task<ActionResult> Post(
            [FromBody]
            CalificacionCreaDTO
            calificacionCreaDTO)
        {
            // validar usuario
            var existeUsuario =
                await _context.Usuarios
                .AnyAsync(x =>
                    x.Id ==
                    calificacionCreaDTO.UsuarioId);

            if (!existeUsuario)
            {
                return BadRequest(
                    "El usuario no existe.");
            }

            var calificacion =
                _mapper.Map<
                    Calificacion>(
                    calificacionCreaDTO);

            _context.Add(calificacion);

            await _context.SaveChangesAsync();

            var calificacionDTO =
                _mapper.Map<
                    CalificacionDTO>(
                    calificacion);

            return CreatedAtRoute(
                "ObtenerCalificacion",
                new
                {
                    id = calificacion.Id
                },
                calificacionDTO);
        }

        // PUT: api/calificacion/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(
            int id,
            CalificacionCreaDTO
            calificacionCreaDTO)
        {
            var existeCalificacion =
                await _context
                .Calificacions
                .AnyAsync(x =>
                    x.Id == id);

            if (!existeCalificacion)
            {
                return NotFound(
                    "La calificación no existe.");
            }

            // validar usuario
            var existeUsuario =
                await _context.Usuarios
                .AnyAsync(x =>
                    x.Id ==
                    calificacionCreaDTO.UsuarioId);

            if (!existeUsuario)
            {
                return BadRequest(
                    "El usuario no existe.");
            }

            var calificacion =
                _mapper.Map<
                    Calificacion>(
                    calificacionCreaDTO);

            calificacion.Id = id;

            _context.Update(
                calificacion);

            await _context
                .SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/calificacion/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult>
            Delete(int id)
        {
            var calificacion =
                await _context
                .Calificacions
                .FindAsync(id);

            if (calificacion == null)
            {
                return NotFound(
                    "La calificación no existe.");
            }

            // validar pedidos asociados
            var tienePedidos =
                await _context.Pedidos
                .AnyAsync(x =>
                    x.CalificacionId
                    == id);

            if (tienePedidos)
            {
                return BadRequest(
                    "No se puede eliminar porque tiene pedidos asociados.");
            }

            _context.Calificacions
                .Remove(calificacion);

            await _context
                .SaveChangesAsync();

            return Ok(
                "Calificación eliminada correctamente.");
        }
    }
}