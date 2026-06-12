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
    public class ClienteController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ClienteController(
            ApplicationDBContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<List<ClienteDTO>>> Get()
        {
            var clientes = await _context.Clientes
                .ToListAsync();

            return _mapper.Map<List<ClienteDTO>>(clientes);
        }

        // GET
        [HttpGet("{id:int}", Name = "ObtenerCliente")]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (cliente == null)
            {
                return NotFound(
                    "Cliente no encontrado.");
            }

            return _mapper.Map<ClienteDTO>(cliente);
        }

        // POST
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Post(
            [FromBody] ClienteCreaDTO clienteCreaDTO)
        {
            // documento repetido
            var existeDocumento =
                await _context.Clientes
                .AnyAsync(x =>
                    x.NroDocumento ==
                    clienteCreaDTO.NroDocumento);

            if (existeDocumento)
            {
                return BadRequest(
                    "Ya existe un cliente con ese número de documento.");
            }

            // validar tipo documento
            var existeTipoDocumento =
                await _context.TipoDocumentos
                .AnyAsync(x =>
                    x.Id ==
                    clienteCreaDTO.TipoDocumentoId);

            if (!existeTipoDocumento)
            {
                return BadRequest("El tipo de documento no existe.");
            }

            // validar extension 
            if (clienteCreaDTO.ExtensionCIId.HasValue)
            {
                var existeExtension =
                    await _context.ExtensionCI
                    .AnyAsync(x =>
                        x.Id ==
                        clienteCreaDTO.ExtensionCIId);

                if (!existeExtension)
                {
                    return BadRequest( "La extensión CI no existe.");
                }
            }

            // validar usuario
            var existeUsuario =
                await _context.Usuarios
                .AnyAsync(x =>
                    x.Id ==
                    clienteCreaDTO.UsuarioId);

            if (!existeUsuario)
            {
                return BadRequest( "El usuario no existe.");
            }

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

            var clienteDTO =
                _mapper.Map<ClienteDTO>(
                    cliente);

            return CreatedAtRoute(
                "ObtenerCliente",
                new { id = cliente.Id },
                clienteDTO);
        }

        // PUT
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

            if (usuarioYaAsignado)
            {
                return BadRequest("Ese usuario ya pertenece a otro cliente.");
            }

            var cliente =
                _mapper.Map<Cliente>(
                    clienteCreaDTO);

            cliente.Id = id;

            _context.Update(cliente);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/cliente/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente =
                await _context.Clientes
                .FindAsync(id);

            if (cliente == null)
            {
                return NotFound( "El cliente no existe.");
            }

            // validar pedidos asociados
            var tienePedidos =
                await _context.Pedidos
                .AnyAsync(x =>
                    x.ClienteId == id);

            if (tienePedidos)
            {
                return BadRequest( "No se puede eliminar el cliente porque tiene pedidos asociados.");
            }

            _context.Clientes
                .Remove(cliente);

            await _context.SaveChangesAsync();

            return Ok(
                "Cliente eliminado correctamente.");
        }
    }
}