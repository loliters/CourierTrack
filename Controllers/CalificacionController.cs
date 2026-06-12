using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAppCourierTrack.DTO;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Todos los endpoints requieren autenticación
    public class CalificacionController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public CalificacionController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Calificacion (cualquier usuario autenticado puede ver todas)
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
            var calificacion = await _context.Calificacions.FirstOrDefaultAsync(x => x.Id == id);
            if (calificacion == null)
                return NotFound("Calificación no encontrada.");
            return Ok(_mapper.Map<CalificacionDTO>(calificacion));
        }

        // POST: api/Calificacion
        [HttpPost]
        public async Task<ActionResult<CalificacionDTO>> Post(CalificacionCreaDTO calificacionCreaDTO)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            // Verificar que el usuario exista
            var existeUsuario = await _context.Usuarios.AnyAsync(u => u.Id == calificacionCreaDTO.UsuarioId);
            if (!existeUsuario)
                return BadRequest("El usuario no existe.");

            // Solo el mismo usuario o un administrador puede crear una calificación
            if (rol != "ADMINISTRADOR" && calificacionCreaDTO.UsuarioId != userId)
                return Forbid("No puedes crear una calificación para otro usuario.");

            var calificacion = _mapper.Map<Calificacion>(calificacionCreaDTO);
            _context.Calificacions.Add(calificacion);
            await _context.SaveChangesAsync();

            var calificacionDTO = _mapper.Map<CalificacionDTO>(calificacion);
            return CreatedAtRoute("ObtenerCalificacion", new { id = calificacion.Id }, calificacionDTO);
        }

        // PUT: api/Calificacion/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, CalificacionCreaDTO calificacionCreaDTO)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            var calificacionExistente = await _context.Calificacions.FindAsync(id);
            if (calificacionExistente == null)
                return NotFound("La calificación no existe.");

            // Solo el propietario o administrador puede modificar
            if (rol != "ADMINISTRADOR" && calificacionExistente.UsuarioId != userId)
                return Forbid("No tienes permiso para modificar esta calificación.");

            // Verificar que el nuevo UsuarioId (si cambia) exista
            if (calificacionExistente.UsuarioId != calificacionCreaDTO.UsuarioId)
            {
                var existeUsuario = await _context.Usuarios.AnyAsync(u => u.Id == calificacionCreaDTO.UsuarioId);
                if (!existeUsuario)
                    return BadRequest("El usuario no existe.");
            }

            _mapper.Map(calificacionCreaDTO, calificacionExistente);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Calificacion/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var calificacion = await _context.Calificacions.FindAsync(id);
            if (calificacion == null)
                return NotFound("La calificación no existe.");

            // Verificar si hay pedidos asociados
            var tienePedidos = await _context.Pedidos.AnyAsync(p => p.CalificacionId == id);
            if (tienePedidos)
                return BadRequest("No se puede eliminar la calificación porque tiene pedidos asociados.");

            _context.Calificacions.Remove(calificacion);
            await _context.SaveChangesAsync();
            return Ok("Calificación eliminada correctamente.");
        }
    }
}