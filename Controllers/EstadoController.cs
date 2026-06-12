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
    public class EstadoController : Controller
    {
        private readonly
            ApplicationDBContext
            _context;

        private readonly
            IMapper _mapper;

        public EstadoController(ApplicationDBContext
            context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<
            List<EstadoDTO>>>
            Get()
        {
            var estados =
                await _context
                .Estados
                .ToListAsync();

            return _mapper.Map<
                List<EstadoDTO>>
                (estados);
        }

        [HttpGet("{id:int}",Name = "ObtenerEstado")]

        public async Task<
            ActionResult<
            EstadoConPedidosDTO>>
            Get(int id)
        {
            var estado =
                await _context
                .Estados
                .Include(x =>
                    x.EstadosPedidos)
                .ThenInclude(x =>
                    x.Pedido)
                .FirstOrDefaultAsync(
                    x => x.Id == id);

            if (estado == null)
                return NotFound(
                    "Estado no encontrado");

            return _mapper.Map<
                EstadoConPedidosDTO>(
                    estado);
        }
    }
}