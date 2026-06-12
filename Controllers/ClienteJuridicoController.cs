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
    public class ClienteJuridicoController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ClienteJuridicoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ClienteJuridico (solo administrador)
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<List<ClienteJuridicoDTO>>> Get()
        {
            var clientesJuridicos = await _context.ClientesJuridicos
                .Include(cj => cj.Cliente)
                    .ThenInclude(c => c.Usuario)
                .ToListAsync();
            return Ok(_mapper.Map<List<ClienteJuridicoDTO>>(clientesJuridicos));
        }

        // GET: api/ClienteJuridico/5
        [HttpGet("{id:int}", Name = "ObtenerClienteJuridico")]
        public async Task<ActionResult<ClienteJuridicoDTO>> Get(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            var clienteJuridico = await _context.ClientesJuridicos
                .Include(cj => cj.Cliente)
                    .ThenInclude(c => c.Usuario)
                .FirstOrDefaultAsync(cj => cj.Id == id);

            if (clienteJuridico == null)
                return NotFound("Cliente jurídico no encontrado.");

            // Validar permiso: administrador o el propio usuario (dueño del cliente)
            if (rol != "ADMINISTRADOR" && clienteJuridico.Cliente?.UsuarioId != userId)
                return Forbid("No tienes permiso para ver este cliente jurídico.");

            return Ok(_mapper.Map<ClienteJuridicoDTO>(clienteJuridico));
        }

        // POST: api/ClienteJuridico (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<ClienteJuridicoDTO>> Post(ClienteJuridicoCreaDTO dto)
        {
            // Validar que el cliente base exista
            if (!await _context.Clientes.AnyAsync(c => c.Id == dto.ClienteId))
                return BadRequest("El cliente no existe.");

            // Validar relación 1:1 (cliente no asignado a otro jurídico)
            if (await _context.ClientesJuridicos.AnyAsync(cj => cj.ClienteId == dto.ClienteId))
                return BadRequest("Ese cliente ya está asignado a un cliente jurídico.");

            // Validar NIT único
            if (await _context.ClientesJuridicos.AnyAsync(cj => cj.Nit == dto.Nit))
                return BadRequest("Ya existe un cliente jurídico con ese NIT.");

            var clienteJuridico = _mapper.Map<ClienteJuridico>(dto);
            _context.ClientesJuridicos.Add(clienteJuridico);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<ClienteJuridicoDTO>(clienteJuridico);
            return CreatedAtRoute("ObtenerClienteJuridico", new { id = clienteJuridico.Id }, resultDto);
        }

        // PUT: api/ClienteJuridico/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, ClienteJuridicoCreaDTO dto)
        {
            var clienteJuridico = await _context.ClientesJuridicos.FindAsync(id);
            if (clienteJuridico == null)
                return NotFound("El cliente jurídico no existe.");

            // Validar que el cliente base exista
            if (!await _context.Clientes.AnyAsync(c => c.Id == dto.ClienteId))
                return BadRequest("El cliente no existe.");

            // Validar que el cliente no esté asignado a otro jurídico (excepto el actual)
            if (await _context.ClientesJuridicos.AnyAsync(cj => cj.ClienteId == dto.ClienteId && cj.Id != id))
                return BadRequest("Ese cliente ya pertenece a otro cliente jurídico.");

            // Validar NIT único (excepto el propio)
            if (await _context.ClientesJuridicos.AnyAsync(cj => cj.Nit == dto.Nit && cj.Id != id))
                return BadRequest("Ya existe un cliente jurídico con ese NIT.");

            _mapper.Map(dto, clienteJuridico);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/ClienteJuridico/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var clienteJuridico = await _context.ClientesJuridicos.FindAsync(id);
            if (clienteJuridico == null)
                return NotFound("El cliente jurídico no existe.");

            _context.ClientesJuridicos.Remove(clienteJuridico);
            await _context.SaveChangesAsync();
            return Ok("Cliente jurídico eliminado correctamente.");
        }
    }
}