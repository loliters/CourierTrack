using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public UsuarioController(
            ApplicationDBContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> Get()
        {
            var usuarios = await _context.Usuarios
                .ToListAsync();

            return _mapper.Map<List<UsuarioDTO>>(
                usuarios);
        }

        // GET
        [HttpGet("{id:int}", Name = "ObtenerUsuario")]
        public async Task<ActionResult<UsuarioDTO>> Get(int id)
        {
            var usuario = await _context.Usuarios
                .Include(x => x.Rol)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (usuario == null)
            {
                return NotFound(
                    "Usuario no encontrado.");
            }

            return _mapper.Map<UsuarioDTO>(
                usuario);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> Post(
            [FromBody] UsuarioCreaDTO usuarioCreaDTO)
        {
            // verificar correo repetido
            var existeCorreo =
                await _context.Usuarios
                .AnyAsync(x =>
                    x.Correo == usuarioCreaDTO.Correo);

            if (existeCorreo)
            {
                return BadRequest(
                    "Ya existe un usuario con ese correo.");
            }

            // verificar rol existente
            var existeRol =
                await _context.Roles
                .AnyAsync(x =>
                    x.Id == usuarioCreaDTO.RolId);

            if (!existeRol)
            {
                return BadRequest(
                    "El rol seleccionado no existe.");
            }

            var usuario =
                _mapper.Map<Usuario>(
                    usuarioCreaDTO);

            _context.Add(usuario);

            await _context.SaveChangesAsync();

            var usuarioDTO =
                _mapper.Map<UsuarioDTO>(
                    usuario);

            return CreatedAtRoute(
                "ObtenerUsuario",
                new { id = usuario.Id },
                usuarioDTO);
        }

        // PUT
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(
            int id,
            UsuarioCreaDTO usuarioCreaDTO)
        {
            var existeUsuario =
                await _context.Usuarios
                .AnyAsync(x => x.Id == id);

            if (!existeUsuario)
            {
                return NotFound(
                    "El usuario no existe.");
            }

            // validar rol
            var existeRol =
                await _context.Roles
                .AnyAsync(x =>
                    x.Id == usuarioCreaDTO.RolId);

            if (!existeRol)
            {
                return BadRequest(
                    "El rol seleccionado no existe.");
            }

            var usuario =
                _mapper.Map<Usuario>(
                    usuarioCreaDTO);

            usuario.Id = id;

            _context.Update(usuario);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario =
                await _context.Usuarios
                .FindAsync(id);

            if (usuario == null)
            {
                return NotFound(
                    "El usuario no existe.");
            }

            _context.Usuarios
                .Remove(usuario);

            await _context.SaveChangesAsync();

            return Ok(
                "Usuario eliminado correctamente.");
        }
    }
}