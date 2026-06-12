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
    [Authorize]
    public class PagoController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public PagoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Pago
        [HttpGet]
        public async Task<ActionResult<List<PagoDTO>>> Get()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            IQueryable<Pago> query = _context.Pagos.Include(p => p.Pedido).ThenInclude(p => p.Cliente);

            if (rol == "ADMINISTRADOR")
            {
                // Admin ve todos
            }
            else if (rol == "CLIENTE")
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.UsuarioId == userId);
                if (cliente == null)
                    return BadRequest("No tienes un perfil de cliente.");
                query = query.Where(p => p.Pedido.ClienteId == cliente.Id);
            }
            else if (rol == "CONDUCTOR")
            {
                var conductor = await _context.Conductores.FirstOrDefaultAsync(c => c.UsuarioId == userId);
                if (conductor == null)
                    return BadRequest("No tienes un perfil de conductor.");
                var pedidosAsignados = await _context.Seguimientos
                    .Where(s => s.ConductorId == conductor.Id)
                    .Select(s => s.PedidoId)
                    .Distinct()
                    .ToListAsync();
                query = query.Where(p => pedidosAsignados.Contains(p.PedidoId));
            }
            else
            {
                return Forbid("Rol no autorizado.");
            }

            var pagos = await query.ToListAsync();
            return Ok(_mapper.Map<List<PagoDTO>>(pagos));
        }

        // GET: api/Pago/5
        [HttpGet("{id:int}", Name = "ObtenerPago")]
        public async Task<ActionResult<PagoDTO>> Get(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            var pago = await _context.Pagos
                .Include(p => p.Pedido)
                    .ThenInclude(p => p.Cliente)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pago == null)
                return NotFound("No existe el pago");

            bool autorizado = false;
            if (rol == "ADMINISTRADOR")
                autorizado = true;
            else if (rol == "CLIENTE")
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.UsuarioId == userId);
                if (cliente != null && pago.Pedido.ClienteId == cliente.Id)
                    autorizado = true;
            }
            else if (rol == "CONDUCTOR")
            {
                var conductor = await _context.Conductores.FirstOrDefaultAsync(c => c.UsuarioId == userId);
                if (conductor != null)
                {
                    bool asignado = await _context.Seguimientos.AnyAsync(s => s.PedidoId == pago.PedidoId && s.ConductorId == conductor.Id);
                    autorizado = asignado;
                }
            }

            if (!autorizado)
                return Forbid("No tienes permiso para ver este pago.");

            return Ok(_mapper.Map<PagoDTO>(pago));
        }

        // POST: api/Pago (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<PagoDTO>> Post(PagoCreaDTO dto)
        {
            // Validar pedido
            if (!await _context.Pedidos.AnyAsync(p => p.Id == dto.PedidoId))
                return BadRequest($"El pedido con Id {dto.PedidoId} no existe.");

            // Validar método de pago
            var metodoPago = await _context.MetodoPagos.FindAsync(dto.MetodoPagoId);
            if (metodoPago == null)
                return BadRequest($"El método de pago con Id {dto.MetodoPagoId} no existe.");

            // Validar estado de pago
            if (!await _context.EstadoPagos.AnyAsync(ep => ep.Id == dto.EstadoPagoId))
                return BadRequest($"El estado de pago con Id {dto.EstadoPagoId} no existe.");

            // Un pedido solo puede tener un pago
            if (await _context.Pagos.AnyAsync(p => p.PedidoId == dto.PedidoId))
                return Conflict("El pedido ya tiene un pago registrado.");

            // Validación específica para Transferencia
            if (metodoPago.Nombre.Equals("Transferencia", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(dto.NumeroTransaccion))
                    return BadRequest("Para transferencia, el número de transacción es obligatorio.");
                if (string.IsNullOrWhiteSpace(dto.CuentaBancaria))
                    return BadRequest("Para transferencia, el número de cuenta bancaria es obligatorio.");
                if (string.IsNullOrWhiteSpace(dto.Banco))
                    return BadRequest("Para transferencia, el nombre del banco es obligatorio.");
            }

            var pago = _mapper.Map<Pago>(dto);
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();

            var pagoDTO = _mapper.Map<PagoDTO>(pago);
            return CreatedAtRoute("ObtenerPago", new { id = pago.Id }, pagoDTO);
        }

        // PUT: api/Pago/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, PagoCreaDTO dto)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
                return NotFound($"No existe el pago con Id {id}");

            // Validar FK si cambian
            if (pago.PedidoId != dto.PedidoId && !await _context.Pedidos.AnyAsync(p => p.Id == dto.PedidoId))
                return BadRequest($"El pedido con Id {dto.PedidoId} no existe.");

            MetodoPago metodoPago = null;
            if (pago.MetodoPagoId != dto.MetodoPagoId)
            {
                metodoPago = await _context.MetodoPagos.FindAsync(dto.MetodoPagoId);
                if (metodoPago == null)
                    return BadRequest($"El método de pago con Id {dto.MetodoPagoId} no existe.");
            }
            else
            {
                metodoPago = await _context.MetodoPagos.FindAsync(pago.MetodoPagoId);
            }

            if (pago.EstadoPagoId != dto.EstadoPagoId && !await _context.EstadoPagos.AnyAsync(ep => ep.Id == dto.EstadoPagoId))
                return BadRequest($"El estado de pago con Id {dto.EstadoPagoId} no existe.");

            // Validar campos de transferencia si corresponde
            if (metodoPago != null && metodoPago.Nombre.Equals("Transferencia", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(dto.NumeroTransaccion))
                    return BadRequest("Para transferencia, el número de transacción es obligatorio.");
                if (string.IsNullOrWhiteSpace(dto.CuentaBancaria))
                    return BadRequest("Para transferencia, el número de cuenta bancaria es obligatorio.");
                if (string.IsNullOrWhiteSpace(dto.Banco))
                    return BadRequest("Para transferencia, el nombre del banco es obligatorio.");
            }

            _mapper.Map(dto, pago);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Pago/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
                return NotFound("No existe el pago");

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}