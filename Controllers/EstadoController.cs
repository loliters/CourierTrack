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
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public EstadoController(
            ApplicationDBContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Estado
        // Público
        [HttpGet]
        public async Task<ActionResult<List<EstadoDTO>>> Get()
        {
            var estados = await _context
                .Estados
                .ToListAsync();

            return _mapper.Map<List<EstadoDTO>>(estados);
        }

        // GEt
        [HttpGet("{id:int}", Name = "ObtenerEstado")]
        public async Task<ActionResult<EstadoConPedidosDTO>> Get(int id)
        {
            var estado = await _context
                .Estados
                .Include(x => x.EstadosPedidos)
                .ThenInclude(x => x.Pedido)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (estado == null)
                return NotFound("Estado no encontrado");

            return _mapper.Map<EstadoConPedidosDTO>(estado);
        }

        // POST: api/Estado
        // Solo Administrador
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Post(
            [FromBody] EstadoCreaDTO estadoCreaDTO)
        {
            var existe = await _context
                .Estados
                .AnyAsync(x => x.Nombre == estadoCreaDTO.Nombre);

            if (existe)
                return BadRequest(
                    $"Ya existe un estado con el nombre {estadoCreaDTO.Nombre}");

            var estado = _mapper.Map<Estado>(estadoCreaDTO);

            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();

            var estadoDTO = _mapper.Map<EstadoDTO>(estado);

            return CreatedAtRoute(
                "ObtenerEstado",
                new { id = estado.Id },
                estadoDTO);
        }

        // PUT: api/Estado/5
        // Solo Administrador
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Put(
            int id,
            [FromBody] EstadoCreaDTO estadoCreaDTO)
        {
            var existeEstado = await _context
                .Estados
                .AnyAsync(x => x.Id == id);

            if (!existeEstado)
                return NotFound("El estado no existe");

            var duplicado = await _context
                .Estados
                .AnyAsync(x =>
                    x.Nombre == estadoCreaDTO.Nombre &&
                    x.Id != id);

            if (duplicado)
                return BadRequest(
                    "Ya existe otro estado con ese nombre");

            var estado = _mapper.Map<Estado>(estadoCreaDTO);
            estado.Id = id;

            _context.Update(estado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Estado/5
        // Solo Administrador
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Delete(int id)
        {
            var estado = await _context
                .Estados
                .FindAsync(id);

            if (estado == null)
                return NotFound(
                    "El estado no existe");

            // Verificar si está relacionado con pedidos
            var tienePedidos = await _context
                .EstadosPedidos
                .AnyAsync(x => x.EstadoId == id);

            if (tienePedidos)
                return BadRequest(
                    "No se puede eliminar el estado porque tiene pedidos asociados");

            _context.Estados.Remove(estado);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}