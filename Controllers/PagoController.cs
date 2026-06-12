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
    public class PagoController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public PagoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Pago (público)
        [HttpGet]
        public async Task<ActionResult<List<PagoDTO>>> Get()
        {
            var pagos = await _context.Pagos.ToListAsync();
            return Ok(_mapper.Map<List<PagoDTO>>(pagos));
        }

        // GET: api/Pago/5 (público)
        [HttpGet("{id:int}", Name = "ObtenerPago")]
        public async Task<ActionResult<PagoDTO>> Get(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
                return NotFound("No existe el pago");

            return Ok(_mapper.Map<PagoDTO>(pago));
        }

        // POST: api/Pago (solo Administrador)
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<PagoDTO>> Post(PagoCreaDTO pagoCreaDTO)
        {
            // Validar que el pedido exista
            var pedidoExiste = await _context.Pedidos.AnyAsync(p => p.Id == pagoCreaDTO.PedidoId);
            if (!pedidoExiste)
                return BadRequest($"El pedido con Id {pagoCreaDTO.PedidoId} no existe.");

            // Validar que el método de pago exista
            var metodoPago = await _context.MetodoPagos.FindAsync(pagoCreaDTO.MetodoPagoId);
            if (metodoPago == null)
                return BadRequest($"El método de pago con Id {pagoCreaDTO.MetodoPagoId} no existe.");

            // Validar que el estado de pago exista
            var estadoPagoExiste = await _context.EstadoPagos.AnyAsync(ep => ep.Id == pagoCreaDTO.EstadoPagoId);
            if (!estadoPagoExiste)
                return BadRequest($"El estado de pago con Id {pagoCreaDTO.EstadoPagoId} no existe.");

            // Validación: un pedido solo puede tener un pago
            var pagoExistente = await _context.Pagos.AnyAsync(p => p.PedidoId == pagoCreaDTO.PedidoId);
            if (pagoExistente)
                return Conflict("El pedido ya tiene un pago registrado.");

            // Validación específica para Transferencia: campos bancarios obligatorios
            if (metodoPago.Nombre.Equals("Transferencia", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(pagoCreaDTO.NumeroTransaccion))
                    return BadRequest("Para transferencia, el número de transacción es obligatorio.");
                if (string.IsNullOrWhiteSpace(pagoCreaDTO.CuentaBancaria))
                    return BadRequest("Para transferencia, el número de cuenta bancaria es obligatorio.");
                if (string.IsNullOrWhiteSpace(pagoCreaDTO.Banco))
                    return BadRequest("Para transferencia, el nombre del banco es obligatorio.");
            }

            var pago = _mapper.Map<Pago>(pagoCreaDTO);
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();

            var pagoDTO = _mapper.Map<PagoDTO>(pago);
            return CreatedAtRoute("ObtenerPago", new { id = pago.Id }, pagoDTO);
        }

        // PUT: api/Pago/5 (solo Administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(int id, PagoCreaDTO pagoCreaDTO)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
                return NotFound($"No existe el pago con Id {id}");

            // Validar existencia de FK (si cambian)
            if (pago.PedidoId != pagoCreaDTO.PedidoId)
            {
                var pedidoExiste = await _context.Pedidos.AnyAsync(p => p.Id == pagoCreaDTO.PedidoId);
                if (!pedidoExiste)
                    return BadRequest($"El pedido con Id {pagoCreaDTO.PedidoId} no existe.");
            }

            MetodoPago? metodoPago = null;
            if (pago.MetodoPagoId != pagoCreaDTO.MetodoPagoId)
            {
                metodoPago = await _context.MetodoPagos.FindAsync(pagoCreaDTO.MetodoPagoId);
                if (metodoPago == null)
                    return BadRequest($"El método de pago con Id {pagoCreaDTO.MetodoPagoId} no existe.");
            }
            else
            {
                metodoPago = await _context.MetodoPagos.FindAsync(pago.MetodoPagoId);
            }

            if (pago.EstadoPagoId != pagoCreaDTO.EstadoPagoId)
            {
                var estadoPagoExiste = await _context.EstadoPagos.AnyAsync(ep => ep.Id == pagoCreaDTO.EstadoPagoId);
                if (!estadoPagoExiste)
                    return BadRequest($"El estado de pago con Id {pagoCreaDTO.EstadoPagoId} no existe.");
            }

            // Si el método de pago es Transferencia, validar campos bancarios
            if (metodoPago != null && metodoPago.Nombre.Equals("Transferencia", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(pagoCreaDTO.NumeroTransaccion))
                    return BadRequest("Para transferencia, el número de transacción es obligatorio.");
                if (string.IsNullOrWhiteSpace(pagoCreaDTO.CuentaBancaria))
                    return BadRequest("Para transferencia, el número de cuenta bancaria es obligatorio.");
                if (string.IsNullOrWhiteSpace(pagoCreaDTO.Banco))
                    return BadRequest("Para transferencia, el nombre del banco es obligatorio.");
            }

            _mapper.Map(pagoCreaDTO, pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Pago/5 (solo Administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
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