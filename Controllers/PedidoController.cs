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
    public class PedidoController: Controller
    {
        private readonly
            ApplicationDBContext
            _context;

        private readonly
            IMapper _mapper;

        public PedidoController(
            ApplicationDBContext
            context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<
            List<
            PedidoConEstadosDTO>>>
            Get()
        {
            var pedidos =
                await _context
                .Pedidos
                .Include(x =>
                    x.EstadosPedidos)
                .ThenInclude(x =>
                    x.Estado)
                .ToListAsync();

            return _mapper.Map<
                List<
                PedidoConEstadosDTO>>
                (pedidos);
        }

        // GET BY ID
        [HttpGet("{id:int}",
            Name =
            "ObtenerPedido")]
        public async Task<
            ActionResult<
            PedidoConEstadosDTO>>
            Get(int id)
        {
            var pedido =
                await _context
                .Pedidos
                .Include(x =>
                    x.EstadosPedidos)
                .ThenInclude(x =>
                    x.Estado)
                .FirstOrDefaultAsync(
                    x => x.Id == id);

            if (pedido == null)
            {
                return NotFound(
                    "Pedido no encontrado");
            }

            return _mapper.Map<
                PedidoConEstadosDTO>(
                    pedido);
        }

        // POST
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult>Post( [FromBody] PedidoCreaDTO pedidoDTO)
        {
            if (pedidoDTO.EstadoIds == null)
            {
                return BadRequest( "Debe asignar al menos un estado.");
            }

            // validar estados
            var estadoIds = await _context.Estados.Where(x =>pedidoDTO.EstadoIds
                    .Contains(x.Id))
                .Select(x =>
                    x.Id)
                .ToListAsync();

            if (estadoIds.Count
                != pedidoDTO
                .EstadoIds.Count)
            {
                return BadRequest( "Se ingresó un estado que no existe.");
            }

            var pedido = _mapper.Map<Pedido>(pedidoDTO);

            _context.Pedidos.Add(pedido);

            await _context.SaveChangesAsync();

            var pedidoMap = _mapper.Map<PedidoDTO>(pedido);

            return CreatedAtRoute(
                "ObtenerPedido",
                new
                {
                    id =
                    pedido.Id
                },
                pedidoMap);
        }

        // PUT
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<
            ActionResult>
            Put(
            int id,
            PedidoCreaDTO
            pedidoDTO)
        {
            var pedido =
                await _context
                .Pedidos
                .Include(x =>
                    x.EstadosPedidos)
                .FirstOrDefaultAsync(
                    x =>
                    x.Id == id);

            if (pedido == null)
            {
                return NotFound("Pedido no existe");
            }

            pedido = _mapper.Map( pedidoDTO,pedido);

            _context.Pedidos.Update(pedido);

            await _context
                .SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<
            ActionResult>
            Delete(int id)
        {
            var existe =await _context.Pedidos .AnyAsync(x =>x.Id == id);

            if (!existe)
            {
                return NotFound(
                    "Pedido no existe");
            }

            _context.Pedidos.Remove(new Pedido{Id = id});

            await _context
                .SaveChangesAsync();

            return NoContent();
        }
    }
}