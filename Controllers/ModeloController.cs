using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCourierTrack.DTO;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeloController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ModeloController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Modelo 
        [HttpGet]
        public async Task<ActionResult<List<ModeloDTO>>> Get()
        {
            var modelos = await _context.Modelos.ToListAsync();
            return Ok(_mapper.Map<List<ModeloDTO>>(modelos));
        }

        // GET: api/Modelo/5 
        [HttpGet("{id:int}", Name = "ObtenerModelo")]
        public async Task<ActionResult<ModeloDTO>> Get(int id)
        {
            var modelo = await _context.Modelos.FindAsync(id);
            if (modelo == null)
                return NotFound("No existe el modelo");

            return Ok(_mapper.Map<ModeloDTO>(modelo));
        }

        // POST: api/Modelo (solo Administrador)
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<ModeloDTO>> Post(ModeloCreaDTO modeloCreaDTO)
        {
            // Verificar que MarcaId exista
            var marcaExiste = await _context.Marcas.AnyAsync(m => m.Id == modeloCreaDTO.MarcaId);
            if (!marcaExiste)
                return BadRequest($"La marca con Id {modeloCreaDTO.MarcaId} no existe.");

            // Verificar que TipoVehiculoId exista
            var tipoVehiculoExiste = await _context.TipoVehiculos.AnyAsync(tv => tv.Id == modeloCreaDTO.TipoVehiculoId);
            if (!tipoVehiculoExiste)
                return BadRequest($"El tipo de vehículo con Id {modeloCreaDTO.TipoVehiculoId} no existe.");

            // Verificar duplicado (nombre + marca)
            var existe = await _context.Modelos.AnyAsync(m =>
                m.Nombre == modeloCreaDTO.Nombre && m.MarcaId == modeloCreaDTO.MarcaId);
            if (existe)
                return BadRequest($"Ya existe el modelo '{modeloCreaDTO.Nombre}' para la marca seleccionada.");

            var modelo = _mapper.Map<Modelo>(modeloCreaDTO);
            _context.Modelos.Add(modelo);
            await _context.SaveChangesAsync();

            var modeloDTO = _mapper.Map<ModeloDTO>(modelo);
            return CreatedAtRoute("ObtenerModelo", new { id = modelo.Id }, modeloDTO);
        }

        // PUT: api/Modelo/5 (solo Administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(int id, ModeloCreaDTO modeloCreaDTO)
        {
            var modelo = await _context.Modelos.FindAsync(id);
            if (modelo == null)
                return NotFound($"No existe el modelo con Id {id}");

            // Validar existencia de Marca y TipoVehiculo
            var marcaExiste = await _context.Marcas.AnyAsync(m => m.Id == modeloCreaDTO.MarcaId);
            if (!marcaExiste)
                return BadRequest($"La marca con Id {modeloCreaDTO.MarcaId} no existe.");

            var tipoVehiculoExiste = await _context.TipoVehiculos.AnyAsync(tv => tv.Id == modeloCreaDTO.TipoVehiculoId);
            if (!tipoVehiculoExiste)
                return BadRequest($"El tipo de vehículo con Id {modeloCreaDTO.TipoVehiculoId} no existe.");

            // Verificar duplicado excluyendo el propio registro
            var duplicado = await _context.Modelos.AnyAsync(m =>
                m.Nombre == modeloCreaDTO.Nombre &&
                m.MarcaId == modeloCreaDTO.MarcaId &&
                m.Id != id);
            if (duplicado)
                return Conflict("Ya existe otro modelo con el mismo nombre para esta marca.");

            _mapper.Map(modeloCreaDTO, modelo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Modelo/5 (solo Administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var modelo = await _context.Modelos.FindAsync(id);
            if (modelo == null)
                return NotFound("No existe el modelo");

            // Verificar si hay vehículos que usan este modelo
            var tieneVehiculo = await _context.Vehiculos.AnyAsync(v => v.ModeloId == id);
            if (tieneVehiculo)
                return BadRequest("No se puede eliminar el modelo porque hay vehículos asociados.");

            _context.Modelos.Remove(modelo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
