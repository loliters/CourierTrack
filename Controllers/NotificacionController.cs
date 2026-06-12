using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCourierTrack.DTO;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public NotificacionController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Notificacion (público)
        [HttpGet]
        public async Task<ActionResult<List<NotificacionDTO>>> Get()
        {
            var notificaciones = await _context.Notificaciones.ToListAsync();
            return Ok(_mapper.Map<List<NotificacionDTO>>(notificaciones));
        }

        // GET: api/Notificacion/5 (público)
        [HttpGet("{id:int}", Name = "ObtenerNotificacion")]
        public async Task<ActionResult<NotificacionDTO>> Get(int id)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion == null)
                return NotFound("No existe la notificación");

            return Ok(_mapper.Map<NotificacionDTO>(notificacion));
        }

        // POST: api/Notificacion (solo Administrador)
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<NotificacionDTO>> Post(NotificacionCreaDTO notificacionCreaDTO)
        {
            // Verificar que el Usuario exista
            var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == notificacionCreaDTO.UsuarioId);
            if (!usuarioExiste)
                return BadRequest($"El usuario con Id {notificacionCreaDTO.UsuarioId} no existe.");

            // Verificar que el Pedido exista
            var pedidoExiste = await _context.Pedidos.AnyAsync(p => p.Id == notificacionCreaDTO.PedidoId);
            if (!pedidoExiste)
                return BadRequest($"El pedido con Id {notificacionCreaDTO.PedidoId} no existe.");

            // Si no se envía el valor de Leida, por defecto false
            var notificacion = _mapper.Map<Notificacion>(notificacionCreaDTO);
            if (notificacionCreaDTO.Leida == null) // el DTO tiene bool, no nullable, pero si el front no lo envía será false.
                notificacion.Leida = false;

            _context.Notificaciones.Add(notificacion);
            await _context.SaveChangesAsync();

            var notificacionDTO = _mapper.Map<NotificacionDTO>(notificacion);
            return CreatedAtRoute("ObtenerNotificacion", new { id = notificacion.Id }, notificacionDTO);
        }

        // PUT: api/Notificacion/5 (solo Administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(int id, NotificacionCreaDTO notificacionCreaDTO)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion == null)
                return NotFound($"No existe la notificación con Id {id}");

            // Validar FK si cambian
            if (notificacion.UsuarioId != notificacionCreaDTO.UsuarioId)
            {
                var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == notificacionCreaDTO.UsuarioId);
                if (!usuarioExiste)
                    return BadRequest($"El usuario con Id {notificacionCreaDTO.UsuarioId} no existe.");
            }

            if (notificacion.PedidoId != notificacionCreaDTO.PedidoId)
            {
                var pedidoExiste = await _context.Pedidos.AnyAsync(p => p.Id == notificacionCreaDTO.PedidoId);
                if (!pedidoExiste)
                    return BadRequest($"El pedido con Id {notificacionCreaDTO.PedidoId} no existe.");
            }

            _mapper.Map(notificacionCreaDTO, notificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Notificacion/5 (solo Administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion == null)
                return NotFound("No existe la notificación");

            // No hay dependencias, se puede eliminar directamente
            _context.Notificaciones.Remove(notificacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
