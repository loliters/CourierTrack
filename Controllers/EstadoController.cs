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
    [Authorize]
    public class EstadoController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public EstadoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Estado (cualquier usuario autenticado)
        [HttpGet]
        public async Task<ActionResult<List<EstadoDTO>>> Get()
        {
            var estados = await _context.Estados.ToListAsync();
            return Ok(_mapper.Map<List<EstadoDTO>>(estados));
        }

        // GET: api/Estado/5
        [HttpGet("{id:int}", Name = "ObtenerEstado")]
        public async Task<ActionResult<EstadoConPedidosDTO>> Get(int id)
        {
            var estado = await _context.Estados
                .Include(e => e.EstadosPedidos)
                    .ThenInclude(ep => ep.Pedido)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estado == null)
                return NotFound("Estado no encontrado");

            return Ok(_mapper.Map<EstadoConPedidosDTO>(estado));
        }

        // POST: api/Estado (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<EstadoDTO>> Post(EstadoCreaDTO dto)
        {
            // Verificar duplicado (nombre único)
            if (await _context.Estados.AnyAsync(e => e.Nombre == dto.Nombre))
                return BadRequest($"Ya existe un estado con el nombre '{dto.Nombre}'.");

            var estado = _mapper.Map<Estado>(dto);
            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();

            var estadoDTO = _mapper.Map<EstadoDTO>(estado);
            return CreatedAtRoute("ObtenerEstado", new { id = estado.Id }, estadoDTO);
        }

        // PUT: api/Estado/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, EstadoCreaDTO dto)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
                return NotFound($"No existe el estado con Id {id}");

            // Verificar duplicado excluyendo el propio registro
            if (await _context.Estados.AnyAsync(e => e.Nombre == dto.Nombre && e.Id != id))
                return Conflict("Ya existe otro estado con ese nombre.");

            _mapper.Map(dto, estado);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Estado/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null)
                return NotFound("No existe el estado");

            // Verificar si hay estados asociados en EstadoPedido
            bool tieneRelaciones = await _context.EstadosPedidos.AnyAsync(ep => ep.EstadoId == id);
            if (tieneRelaciones)
                return BadRequest("No se puede eliminar el estado porque está siendo utilizado en pedidos.");

            _context.Estados.Remove(estado);
            await _context.SaveChangesAsync();
            return Ok("Estado eliminado correctamente.");
        }
    }
}