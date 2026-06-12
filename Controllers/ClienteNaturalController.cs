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
    public class ClienteNaturalController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ClienteNaturalController(
            ApplicationDBContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<List<ClienteNaturalDTO>>> Get()
        {
            var clientesNaturales =
                await _context.ClientesNatural
                .ToListAsync();

            return _mapper.Map<
                List<ClienteNaturalDTO>>(
                    clientesNaturales);
        }

        // GET
        [HttpGet("{id:int}",
            Name = "ObtenerClienteNatural")]
        public async Task<ActionResult<ClienteNaturalDTO>>
            Get(int id)
        {
            var clienteNatural =
                await _context.ClientesNatural
                .FirstOrDefaultAsync(x =>
                    x.Id == id);

            if (clienteNatural == null)
            {
                return NotFound(
                    "Cliente natural no encontrado.");
            }

            return _mapper.Map<
                ClienteNaturalDTO>(
                    clienteNatural);
        }

        // POST
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Post(
            [FromBody]
            ClienteNaturalCreaDTO
            clienteNaturalCreaDTO)
        {
            // validar género
            var existeGenero =
                await _context.Generos
                .AnyAsync(x =>
                    x.Id ==
                    clienteNaturalCreaDTO.GeneroId);

            if (!existeGenero)
            {
                return BadRequest("El género no existe.");
            }

            // validar cliente
            var existeCliente =
                await _context.Clientes
                .AnyAsync(x =>
                    x.Id ==
                    clienteNaturalCreaDTO.ClienteId);

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

            var clienteNaturalDTO =
                _mapper.Map<
                    ClienteNaturalDTO>(
                    clienteNatural);

            return CreatedAtRoute(
                "ObtenerClienteNatural",
                new
                {
                    id = clienteNatural.Id
                },
                clienteNaturalDTO);
        }

        // PUT
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

            _context.Update(
                clienteNatural);

            await _context
                .SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/clientenatural/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult>
            Delete(int id)
        {
            var clienteNatural =
                await _context
                .ClientesNatural
                .FindAsync(id);

            if (clienteNatural == null)
            {
                return NotFound(
                    "El cliente natural no existe.");
            }

            _context.ClientesNatural
                .Remove(clienteNatural);

            await _context
                .SaveChangesAsync();

            return Ok(
                "Cliente natural eliminado correctamente.");
        }
    }
}