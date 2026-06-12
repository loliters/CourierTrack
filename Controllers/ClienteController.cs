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
    public class ClienteController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ClienteController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Cliente (solo administrador)
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<List<ClienteDTO>>> Get()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return Ok(_mapper.Map<List<ClienteDTO>>(clientes));
        }

        // GET: api/Cliente/5
        [HttpGet("{id:int}", Name = "ObtenerCliente")]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            var cliente = await _context.Clientes
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null)
                return NotFound("Cliente no encontrado.");

            // Validar permiso: administrador o el propio cliente
            if (rol != "ADMINISTRADOR" && cliente.UsuarioId != userId)
                return Forbid("No tienes permiso para ver este cliente.");

            return Ok(_mapper.Map<ClienteDTO>(cliente));
        }

        // POST: api/Cliente (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Post(
            [FromBody] ClienteCreaDTO clienteCreaDTO)
        {
            // Documento repetido
            if (await _context.Clientes.AnyAsync(c => c.NroDocumento == clienteCreaDTO.NroDocumento))
                return BadRequest("Ya existe un cliente con ese número de documento.");

            // Validar tipo documento
            if (!await _context.TipoDocumentos.AnyAsync(td => td.Id == clienteCreaDTO.TipoDocumentoId))
                return BadRequest("El tipo de documento no existe.");

            // Validar extensión (si se proporciona)
            if (clienteCreaDTO.ExtensionCIId.HasValue &&
                !await _context.ExtensionCI.AnyAsync(ext => ext.Id == clienteCreaDTO.ExtensionCIId))
                return BadRequest("La extensión CI no existe.");

            // Validar usuario existente
            if (!await _context.Usuarios.AnyAsync(u => u.Id == clienteCreaDTO.UsuarioId))
                return BadRequest("El usuario no existe.");

            // Validar que el usuario no esté ya asignado como cliente
            if (await _context.Clientes.AnyAsync(c => c.UsuarioId == clienteCreaDTO.UsuarioId))
                return BadRequest("Ese usuario ya está asignado a otro cliente.");

            // Validar tipo de cliente
            if (!await _context.TipoClientes.AnyAsync(tc => tc.Id == clienteCreaDTO.TipoClienteId))
                return BadRequest("El tipo de cliente no existe.");

            // validar usuario no usado (1:1)
            var usuarioYaAsignado =
                await _context.Clientes
                .AnyAsync(x =>
                    x.UsuarioId ==
                    clienteCreaDTO.UsuarioId);

            if (usuarioYaAsignado)
            {
                return BadRequest(
                    "Ese usuario ya está asignado a otro cliente.");
            }

            // validar tipo cliente
            var existeTipoCliente = await _context.TipoClientes.AnyAsync(x => x.Id == clienteCreaDTO.TipoClienteId);

            if (!existeTipoCliente)
            {
                return BadRequest( "El tipo de cliente no existe.");
            }

            var cliente =
                _mapper.Map<Cliente>(
                    clienteCreaDTO);

            _context.Add(cliente);

            await _context.SaveChangesAsync();

            var clienteDTO = _mapper.Map<ClienteDTO>(cliente);
            return CreatedAtRoute("ObtenerCliente", new { id = cliente.Id }, clienteDTO);
        }

        // PUT: api/Cliente/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Put(
            int id,
            ClienteCreaDTO clienteCreaDTO)
        {
            var existeCliente =
                await _context.Clientes
                .AnyAsync(x => x.Id == id);

            if (!existeCliente)
            {
                return NotFound(
                    "El cliente no existe.");
            }

            // validar usuario 1:1
            var usuarioYaAsignado =
                await _context.Clientes
                .AnyAsync(x =>
                    x.UsuarioId ==
                    clienteCreaDTO.UsuarioId
                    && x.Id != id);

            // Verificar que el usuario no esté asignado a otro cliente (excepto el actual)
            if (await _context.Clientes.AnyAsync(c => c.UsuarioId == clienteCreaDTO.UsuarioId && c.Id != id))
                return BadRequest("Ese usuario ya pertenece a otro cliente.");

            _mapper.Map(clienteCreaDTO, clienteExistente);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Cliente/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound("El cliente no existe.");

            // Verificar si tiene pedidos asociados
            if (await _context.Pedidos.AnyAsync(p => p.ClienteId == id))
                return BadRequest("No se puede eliminar el cliente porque tiene pedidos asociados.");

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return Ok("Cliente eliminado correctamente.");
        }
    }
}