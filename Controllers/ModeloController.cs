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
    [Authorize] // Todos los endpoints requieren autenticación
    public class ModeloController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ModeloController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Modelo (cualquier usuario autenticado)
        [HttpGet]
        public async Task<ActionResult<List<ModeloDTO>>> Get()
        {
            var modelos = await _context.Modelos
                .Include(m => m.Marca)
                .Include(m => m.TipoVehiculo)
                .ToListAsync();
            return Ok(_mapper.Map<List<ModeloDTO>>(modelos));
        }

        // GET: api/Modelo/5
        [HttpGet("{id:int}", Name = "ObtenerModelo")]
        public async Task<ActionResult<ModeloDTO>> Get(int id)
        {
            var modelo = await _context.Modelos
                .Include(m => m.Marca)
                .Include(m => m.TipoVehiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modelo == null)
                return NotFound("No existe el modelo");
            return Ok(_mapper.Map<ModeloDTO>(modelo));
        }

        // POST: api/Modelo (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<ModeloDTO>> Post(ModeloCreaDTO dto)
        {
            // Verificar que MarcaId exista
            if (!await _context.Marcas.AnyAsync(m => m.Id == dto.MarcaId))
                return BadRequest($"La marca con Id {dto.MarcaId} no existe.");

            // Verificar que TipoVehiculoId exista
            if (!await _context.TipoVehiculos.AnyAsync(tv => tv.Id == dto.TipoVehiculoId))
                return BadRequest($"El tipo de vehículo con Id {dto.TipoVehiculoId} no existe.");

            // Verificar duplicado (nombre + marca)
            if (await _context.Modelos.AnyAsync(m => m.Nombre == dto.Nombre && m.MarcaId == dto.MarcaId))
                return BadRequest($"Ya existe el modelo '{dto.Nombre}' para la marca seleccionada.");

            var modelo = _mapper.Map<Modelo>(dto);
            _context.Modelos.Add(modelo);
            await _context.SaveChangesAsync();

            var modeloDTO = _mapper.Map<ModeloDTO>(modelo);
            return CreatedAtRoute("ObtenerModelo", new { id = modelo.Id }, modeloDTO);
        }

        // PUT: api/Modelo/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, ModeloCreaDTO dto)
        {
            var modelo = await _context.Modelos.FindAsync(id);
            if (modelo == null)
                return NotFound($"No existe el modelo con Id {id}");

            // Validar existencia de Marca y TipoVehiculo
            if (!await _context.Marcas.AnyAsync(m => m.Id == dto.MarcaId))
                return BadRequest($"La marca con Id {dto.MarcaId} no existe.");

            if (!await _context.TipoVehiculos.AnyAsync(tv => tv.Id == dto.TipoVehiculoId))
                return BadRequest($"El tipo de vehículo con Id {dto.TipoVehiculoId} no existe.");

            // Verificar duplicado excluyendo el propio registro
            if (await _context.Modelos.AnyAsync(m => m.Nombre == dto.Nombre && m.MarcaId == dto.MarcaId && m.Id != id))
                return Conflict("Ya existe otro modelo con el mismo nombre para esta marca.");

            _mapper.Map(dto, modelo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Modelo/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var modelo = await _context.Modelos.FindAsync(id);
            if (modelo == null)
                return NotFound("No existe el modelo");

            // Verificar si hay vehículos que usan este modelo
            if (await _context.Vehiculos.AnyAsync(v => v.ModeloId == id))
                return BadRequest("No se puede eliminar el modelo porque hay vehículos asociados.");

            _context.Modelos.Remove(modelo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}