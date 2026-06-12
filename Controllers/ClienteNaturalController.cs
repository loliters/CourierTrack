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
    public class ClienteNaturalController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ClienteNaturalController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ClienteNatural (solo administrador)
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<List<ClienteNaturalDTO>>> Get()
        {
            var clientesNaturales = await _context.ClientesNatural
                .Include(cn => cn.Cliente)
                    .ThenInclude(c => c.Usuario)
                .ToListAsync();
            return Ok(_mapper.Map<List<ClienteNaturalDTO>>(clientesNaturales));
        }

        // GET: api/ClienteNatural/5
        [HttpGet("{id:int}", Name = "ObtenerClienteNatural")]
        public async Task<ActionResult<ClienteNaturalDTO>> Get(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            var clienteNatural = await _context.ClientesNatural
                .Include(cn => cn.Cliente)
                    .ThenInclude(c => c.Usuario)
                .FirstOrDefaultAsync(cn => cn.Id == id);

            if (clienteNatural == null)
                return NotFound("Cliente natural no encontrado.");

            // Validar permiso: administrador o el propio usuario (dueño del cliente)
            if (rol != "ADMINISTRADOR" && clienteNatural.Cliente?.UsuarioId != userId)
                return Forbid("No tienes permiso para ver este cliente natural.");

            return Ok(_mapper.Map<ClienteNaturalDTO>(clienteNatural));
        }

        // POST: api/ClienteNatural (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Post(
            [FromBody]
            ClienteNaturalCreaDTO
            clienteNaturalCreaDTO)
        {
            // Validar género
            if (!await _context.Generos.AnyAsync(g => g.Id == dto.GeneroId))
                return BadRequest("El género no existe.");

            if (!existeGenero)
            {
                return BadRequest("El género no existe.");
            }

            // Validar relación 1:1 (cliente no asignado a otro natural)
            if (await _context.ClientesNatural.AnyAsync(cn => cn.ClienteId == dto.ClienteId))
                return BadRequest("Ese cliente ya está asignado a un cliente natural.");

            if (!existeCliente)
            {
                return BadRequest("El cliente no existe.");
            }

            // validar cliente no usado (1:1)
            var clienteYaAsignado =
                await _context.ClientesNatural
                .AnyAsync(x =>
                    x.ClienteId ==
                    clienteNaturalCreaDTO.ClienteId);

            if (clienteYaAsignado)
            {
                return BadRequest("Ese cliente ya está asignado a un cliente natural.");
            }

            var clienteNatural =
                _mapper.Map<
                    ClienteNatural>(
                    clienteNaturalCreaDTO);

            _context.Add(clienteNatural);

            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<ClienteNaturalDTO>(clienteNatural);
            return CreatedAtRoute("ObtenerClienteNatural", new { id = clienteNatural.Id }, resultDto);
        }

        // PUT: api/ClienteNatural/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Put(
            int id,
            ClienteNaturalCreaDTO
            clienteNaturalCreaDTO)
        {
            var existeClienteNatural =
                await _context.ClientesNatural
                .AnyAsync(x =>
                    x.Id == id);

            if (!existeClienteNatural)
            {
                return NotFound(
                    "El cliente natural no existe.");
            }

            // validar cliente 1:1
            var clienteYaAsignado =
                await _context.ClientesNatural
                .AnyAsync(x =>
                    x.ClienteId ==
                    clienteNaturalCreaDTO.ClienteId
                    && x.Id != id);

            if (clienteYaAsignado)
            {
                return BadRequest(
                    "Ese cliente ya pertenece a otro cliente natural.");
            }

            var clienteNatural =
                _mapper.Map<
                    ClienteNatural>(
                    clienteNaturalCreaDTO);

            clienteNatural.Id = id;

            // Validar que el cliente base exista
            if (!await _context.Clientes.AnyAsync(c => c.Id == dto.ClienteId))
                return BadRequest("El cliente no existe.");

            // Validar que el cliente no esté asignado a otro natural (excepto el actual)
            if (await _context.ClientesNatural.AnyAsync(cn => cn.ClienteId == dto.ClienteId && cn.Id != id))
                return BadRequest("Ese cliente ya pertenece a otro cliente natural.");

            _mapper.Map(dto, clienteNatural);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/ClienteNatural/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult>
            Delete(int id)
        {
            var clienteNatural = await _context.ClientesNatural.FindAsync(id);
            if (clienteNatural == null)
                return NotFound("El cliente natural no existe.");

            _context.ClientesNatural.Remove(clienteNatural);
            await _context.SaveChangesAsync();
            return Ok("Cliente natural eliminado correctamente.");
        }
    }
}