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
    public class PedidoController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public PedidoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Pedido
        [HttpGet]
        public async Task<ActionResult<List<PedidoConEstadosDTO>>> Get()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            IQueryable<Pedido> query = _context.Pedidos
                .Include(p => p.EstadosPedidos)
                    .ThenInclude(ep => ep.Estado)
                .Include(p => p.Cliente)
                    .ThenInclude(c => c.Usuario);

            if (rol == "ADMINISTRADOR")
            {
                // Admin ve todos
            }
            else if (rol == "CLIENTE")
            {
                // Cliente ve sus propios pedidos
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.UsuarioId == userId);
                if (cliente == null)
                    return BadRequest("No tienes un perfil de cliente.");
                query = query.Where(p => p.ClienteId == cliente.Id);
            }
            else if (rol == "CONDUCTOR")
            {
                // Conductor ve los pedidos asignados a él
                var conductor = await _context.Conductores.FirstOrDefaultAsync(c => c.UsuarioId == userId);
                if (conductor == null)
                    return BadRequest("No tienes un perfil de conductor.");
                var pedidosAsignados = await _context.Seguimientos
                    .Where(s => s.ConductorId == conductor.Id)
                    .Select(s => s.PedidoId)
                    .Distinct()
                    .ToListAsync();
                query = query.Where(p => pedidosAsignados.Contains(p.Id));
            }
            else
            {
                return Forbid("Rol no autorizado.");
            }

            var pedidos = await query.ToListAsync();
            return Ok(_mapper.Map<List<PedidoConEstadosDTO>>(pedidos));
        }

        // GET: api/Pedido/5
        [HttpGet("{id:int}", Name = "ObtenerPedido")]
        public async Task<ActionResult<PedidoConEstadosDTO>> Get(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            var pedido = await _context.Pedidos
                .Include(p => p.EstadosPedidos)
                    .ThenInclude(ep => ep.Estado)
                .Include(p => p.Cliente)
                    .ThenInclude(c => c.Usuario)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return NotFound("Pedido no encontrado.");

            // Validar permiso según rol
            if (rol == "ADMINISTRADOR")
            {
                // Admin puede ver cualquiera
            }
            else if (rol == "CLIENTE")
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.UsuarioId == userId);
                if (cliente == null || pedido.ClienteId != cliente.Id)
                    return Forbid("No tienes permiso para ver este pedido.");
            }
            else if (rol == "CONDUCTOR")
            {
                var conductor = await _context.Conductores.FirstOrDefaultAsync(c => c.UsuarioId == userId);
                if (conductor == null)
                    return Forbid("No tienes un perfil de conductor.");
                bool asignado = await _context.Seguimientos.AnyAsync(s => s.PedidoId == id && s.ConductorId == conductor.Id);
                if (!asignado)
                    return Forbid("No tienes permiso para ver este pedido.");
            }
            else
            {
                return Forbid("Rol no autorizado.");
            }

            return Ok(_mapper.Map<PedidoConEstadosDTO>(pedido));
        }

        // POST: api/Pedido
        [HttpPost]
        public async Task<ActionResult<PedidoDTO>> Post(PedidoCreaDTO pedidoDTO)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;

            // Validar estados
            if (pedidoDTO.EstadoIds == null || !pedidoDTO.EstadoIds.Any())
                return BadRequest("Debe asignar al menos un estado.");

            var estadoIds = await _context.Estados
                .Where(e => pedidoDTO.EstadoIds.Contains(e.Id))
                .Select(e => e.Id)
                .ToListAsync();

            if (estadoIds.Count != pedidoDTO.EstadoIds.Count)
                return BadRequest("Se ingresó un estado que no existe.");

            // Determinar el ClienteId según el rol
            int clienteId;
            if (rol == "ADMINISTRADOR")
            {
                // Admin puede crear pedido para cualquier cliente (debe enviar ClienteId en el DTO)
                if (pedidoDTO.ClienteId == null)
                    return BadRequest("Para administradores, debe proporcionar el ClienteId.");
                clienteId = pedidoDTO.ClienteId.Value;
                // Validar que el cliente exista
                if (!await _context.Clientes.AnyAsync(c => c.Id == clienteId))
                    return BadRequest("El cliente especificado no existe.");
            }
            else if (rol == "CLIENTE")
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.UsuarioId == userId);
                if (cliente == null)
                    return BadRequest("No tienes un perfil de cliente.");
                clienteId = cliente.Id;
                // Asegurar que el DTO no envíe un ClienteId diferente
                if (pedidoDTO.ClienteId != null && pedidoDTO.ClienteId != clienteId)
                    return BadRequest("No puedes crear un pedido para otro cliente.");
            }
            else
            {
                return Forbid("Solo Administradores y Clientes pueden crear pedidos.");
            }

            var pedido = _mapper.Map<Pedido>(pedidoDTO);
            pedido.ClienteId = clienteId;

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            var pedidoMap = _mapper.Map<PedidoDTO>(pedido);
            return CreatedAtRoute("ObtenerPedido", new { id = pedido.Id }, pedidoMap);
        }

        // PUT: api/Pedido/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")] // Solo administrador puede actualizar pedidos
        public async Task<IActionResult> Put(int id, PedidoCreaDTO pedidoDTO)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.EstadosPedidos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
                return NotFound("Pedido no existe.");

            // Validar estados
            if (pedidoDTO.EstadoIds == null || !pedidoDTO.EstadoIds.Any())
                return BadRequest("Debe asignar al menos un estado.");

            var estadoIds = await _context.Estados
                .Where(e => pedidoDTO.EstadoIds.Contains(e.Id))
                .Select(e => e.Id)
                .ToListAsync();

            if (estadoIds.Count != pedidoDTO.EstadoIds.Count)
                return BadRequest("Se ingresó un estado que no existe.");

            // Mapear y actualizar
            _mapper.Map(pedidoDTO, pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Pedido/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")] // Solo administrador puede eliminar
        public async Task<IActionResult> Delete(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
                return NotFound("Pedido no existe.");

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}