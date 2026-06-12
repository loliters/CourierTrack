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
    public class NotificacionController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public NotificacionController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Notificacion (solo notificaciones del usuario autenticado)
        [HttpGet]
        public async Task<ActionResult<List<NotificacionDTO>>> Get()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            IQueryable<Notificacion> query = _context.Notificaciones;

            if (rol != "ADMINISTRADOR")
                query = query.Where(n => n.UsuarioId == userId);

            var notificaciones = await query.ToListAsync();
            return Ok(_mapper.Map<List<NotificacionDTO>>(notificaciones));
        }

        // GET: api/Notificacion/5
        [HttpGet("{id:int}", Name = "ObtenerNotificacion")]
        public async Task<ActionResult<NotificacionDTO>> Get(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion == null)
                return NotFound("No existe la notificación");

            if (rol != "ADMINISTRADOR" && notificacion.UsuarioId != userId)
                return Forbid("No tienes permiso para ver esta notificación.");

            return Ok(_mapper.Map<NotificacionDTO>(notificacion));
        }

        // POST: api/Notificacion (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<NotificacionDTO>> Post(NotificacionCreaDTO dto)
        {
            // Verificar que el usuario exista
            if (!await _context.Usuarios.AnyAsync(u => u.Id == dto.UsuarioId))
                return BadRequest($"El usuario con Id {dto.UsuarioId} no existe.");

            // Verificar que el pedido exista
            if (!await _context.Pedidos.AnyAsync(p => p.Id == dto.PedidoId))
                return BadRequest($"El pedido con Id {dto.PedidoId} no existe.");

            var notificacion = _mapper.Map<Notificacion>(dto);
            notificacion.Leida = false; // Por defecto no leída

            _context.Notificaciones.Add(notificacion);
            await _context.SaveChangesAsync();

            var notificacionDTO = _mapper.Map<NotificacionDTO>(notificacion);
            return CreatedAtRoute("ObtenerNotificacion", new { id = notificacion.Id }, notificacionDTO);
        }

        // PUT: api/Notificacion/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, NotificacionCreaDTO dto)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion == null)
                return NotFound($"No existe la notificación con Id {id}");

            // Validar FK si cambian
            if (notificacion.UsuarioId != dto.UsuarioId)
            {
                if (!await _context.Usuarios.AnyAsync(u => u.Id == dto.UsuarioId))
                    return BadRequest($"El usuario con Id {dto.UsuarioId} no existe.");
            }

            if (notificacion.PedidoId != dto.PedidoId)
            {
                if (!await _context.Pedidos.AnyAsync(p => p.Id == dto.PedidoId))
                    return BadRequest($"El pedido con Id {dto.PedidoId} no existe.");
            }

            _mapper.Map(dto, notificacion);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Notificacion/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion == null)
                return NotFound("No existe la notificación");

            _context.Notificaciones.Remove(notificacion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}