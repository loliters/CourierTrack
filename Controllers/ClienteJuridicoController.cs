using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCourierTrack.DTO;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteJuridicoController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ClienteJuridicoController(
            ApplicationDBContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<List<ClienteJuridicoDTO>>> Get()
        {
            var clientesJuridicos =
                await _context.ClientesJuridicos
                .ToListAsync();

            return _mapper.Map<
                List<ClienteJuridicoDTO>>(
                    clientesJuridicos);
        }

        // GET
        [HttpGet("{id:int}",
            Name = "ObtenerClienteJuridico")]
        public async Task<ActionResult<
            ClienteJuridicoDTO>> Get(int id)
        {
            var clienteJuridico =
                await _context.ClientesJuridicos
                .FirstOrDefaultAsync(x =>
                    x.Id == id);

            if (clienteJuridico == null)
            {
                return NotFound(
                    "Cliente jurídico no encontrado.");
            }

            return _mapper.Map<
                ClienteJuridicoDTO>(
                    clienteJuridico);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> Post(
            [FromBody]
            ClienteJuridicoCreaDTO
            clienteJuridicoCreaDTO)
        {
            // validar cliente
            var existeCliente =
                await _context.Clientes
                .AnyAsync(x =>
                    x.Id ==
                    clienteJuridicoCreaDTO.ClienteId);

            if (!existeCliente)
            {
                return BadRequest(
                    "El cliente no existe.");
            }

            // validar relación 1:1
            var clienteYaAsignado =
                await _context.ClientesJuridicos
                .AnyAsync(x =>
                    x.ClienteId ==
                    clienteJuridicoCreaDTO.ClienteId);

            if (clienteYaAsignado)
            {
                return BadRequest(
                    "Ese cliente ya está asignado a un cliente jurídico.");
            }

            // validar NIT repetido
            var existeNit =
                await _context.ClientesJuridicos
                .AnyAsync(x =>
                    x.Nit ==
                    clienteJuridicoCreaDTO.Nit);

            if (existeNit)
            {
                return BadRequest(
                    "Ya existe un cliente jurídico con ese NIT.");
            }

            var clienteJuridico =
                _mapper.Map<
                    ClienteJuridico>(
                    clienteJuridicoCreaDTO);

            _context.Add(clienteJuridico);

            await _context.SaveChangesAsync();

            var clienteJuridicoDTO =
                _mapper.Map<
                    ClienteJuridicoDTO>(
                    clienteJuridico);

            return CreatedAtRoute(
                "ObtenerClienteJuridico",
                new
                {
                    id = clienteJuridico.Id
                },
                clienteJuridicoDTO);
        }

        // PUT
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(
            int id,
            ClienteJuridicoCreaDTO
            clienteJuridicoCreaDTO)
        {
            var existeClienteJuridico =
                await _context.ClientesJuridicos
                .AnyAsync(x =>
                    x.Id == id);

            if (!existeClienteJuridico)
            {
                return NotFound(
                    "El cliente jurídico no existe.");
            }

            // validar cliente 1:1
            var clienteYaAsignado =
                await _context.ClientesJuridicos
                .AnyAsync(x =>
                    x.ClienteId ==
                    clienteJuridicoCreaDTO.ClienteId
                    && x.Id != id);

            if (clienteYaAsignado)
            {
                return BadRequest(
                    "Ese cliente ya pertenece a otro cliente jurídico.");
            }

            // validar NIT repetido
            var existeNit =
                await _context.ClientesJuridicos
                .AnyAsync(x =>
                    x.Nit ==
                    clienteJuridicoCreaDTO.Nit
                    && x.Id != id);

            if (existeNit)
            {
                return BadRequest(
                    "Ya existe un cliente jurídico con ese NIT.");
            }

            var clienteJuridico =
                _mapper.Map<
                    ClienteJuridico>(
                    clienteJuridicoCreaDTO);

            clienteJuridico.Id = id;

            _context.Update(
                clienteJuridico);

            await _context
                .SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult>
            Delete(int id)
        {
            var clienteJuridico =
                await _context
                .ClientesJuridicos
                .FindAsync(id);

            if (clienteJuridico == null)
            {
                return NotFound(
                    "El cliente jurídico no existe.");
            }

            _context.ClientesJuridicos
                .Remove(clienteJuridico);

            await _context
                .SaveChangesAsync();

            return Ok(
                "Cliente jurídico eliminado correctamente.");
        }
    }
}