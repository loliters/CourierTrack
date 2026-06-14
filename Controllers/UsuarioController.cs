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
    public class UsuarioController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public UsuarioController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Usuario (solo Administrador)
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<List<UsuarioDTO>>> Get()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.UsuariosUbicaciones)
                    .ThenInclude(uu => uu.Ubicacion)
                .ToListAsync();
            return Ok(_mapper.Map<List<UsuarioDTO>>(usuarios));
        }

        // GET: api/Usuario/5 (autenticado, solo propio perfil o administrador)
        [HttpGet("{id:int}", Name = "ObtenerUsuario")]
        [Authorize]
        public async Task<ActionResult<UsuarioConUbicacionesDTO>> Get(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!User.IsInRole("ADMINISTRADOR") && userId != id.ToString())
                return Forbid("No tienes permiso para ver este usuario.");

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.UsuariosUbicaciones)
                    .ThenInclude(uu => uu.Ubicacion)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null) return NotFound();
            return Ok(_mapper.Map<UsuarioConUbicacionesDTO>(usuario));
        }

        // POST: api/Usuario (registro público)
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioDTO>> Post(UsuarioCreaDTO dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Correo == dto.Correo))
                return BadRequest("Correo ya registrado.");

            if (!await _context.Roles.AnyAsync(r => r.Id == dto.RolId))
                return BadRequest("Rol inválido.");

            foreach (int id in dto.UbicacionesIds)
            {
                if (!await _context.Ubicaciones.AnyAsync(u => u.Id == id))
                    return BadRequest($"Ubicación con Id {id} no existe.");
            }

            var usuario = _mapper.Map<Usuario>(dto);
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            return CreatedAtRoute("ObtenerUsuario", new { id = usuario.Id }, usuarioDTO);
        }

        // PUT: api/Usuario/5 (autenticado, solo propio perfil o administrador)
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UsuarioCreaDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!User.IsInRole("ADMINISTRADOR") && userId != id.ToString())
                return Forbid("No tienes permiso para modificar este usuario.");

            var usuario = await _context.Usuarios
                .Include(u => u.UsuariosUbicaciones)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null) return NotFound("Usuario no encontrado.");

            if (!await _context.Roles.AnyAsync(r => r.Id == dto.RolId))
                return BadRequest("El rol seleccionado no existe.");

            foreach (int ubicacionId in dto.UbicacionesIds)
            {
                if (!await _context.Ubicaciones.AnyAsync(u => u.Id == ubicacionId))
                    return BadRequest($"La ubicación con Id {ubicacionId} no existe.");
            }

            _mapper.Map(dto, usuario);

            if (!string.IsNullOrWhiteSpace(dto.Password))
                usuario.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Sincronizar ubicaciones
            var idsActuales = usuario.UsuariosUbicaciones.Select(uu => uu.UbicacionId).ToList();
            var idsNuevos = dto.UbicacionesIds;

            foreach (var uu in usuario.UsuariosUbicaciones.ToList())
            {
                if (!idsNuevos.Contains(uu.UbicacionId))
                    usuario.UsuariosUbicaciones.Remove(uu);
            }

            foreach (int ubicacionId in idsNuevos)
            {
                if (!idsActuales.Contains(ubicacionId))
                    usuario.UsuariosUbicaciones.Add(new UsuarioUbicacion { UbicacionId = ubicacionId });
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Usuario/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.UsuariosUbicaciones)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null) return NotFound("Usuario no encontrado.");

            bool esCliente = await _context.Clientes.AnyAsync(c => c.UsuarioId == id);
            bool esConductor = await _context.Conductores.AnyAsync(c => c.UsuarioId == id);
            if (esCliente || esConductor)
                return BadRequest("No se puede eliminar el usuario porque tiene datos asociados como cliente o conductor.");

            _context.UsuariosUbicaciones.RemoveRange(usuario.UsuariosUbicaciones);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return Ok("Usuario eliminado correctamente.");
        }
    }
}