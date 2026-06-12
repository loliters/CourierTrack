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
    public class VehiculoController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public VehiculoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Vehiculo 
        [HttpGet]
        public async Task<ActionResult<List<VehiculoDTO>>> Get()
        {
            var vehiculos = await _context.Vehiculos
            .Include(v => v.Modelo)
            .Include(v => v.Color)
            .Include(v => v.AnioVehiculo)
            .Include(v => v.Conductor)
                .ThenInclude(c => c.Usuario)   
            .ToListAsync();

            var vehiculosDTO = _mapper.Map<List<VehiculoDTO>>(vehiculos);
            return Ok(vehiculosDTO);
        }

        // GET: api/Vehiculo/5 
        [HttpGet("{id:int}", Name = "ObtenerVehiculo")]
        public async Task<ActionResult<VehiculoDTO>> Get(int id)
        {
            var vehiculo = await _context.Vehiculos
                .Include(v => v.Modelo)
                .Include(v => v.Color)
                .Include(v => v.AnioVehiculo)
                .Include(v => v.Conductor)
                    .ThenInclude(c => c.Usuario)   
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vehiculo == null)
                return NotFound("No existe el vehículo");

            var vehiculoDTO = _mapper.Map<VehiculoDTO>(vehiculo);
            return Ok(vehiculoDTO);
        }

        // POST: api/Vehiculo (solo Administrador)
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<VehiculoDTO>> Post(VehiculoCreaDTO vehiculoCreaDTO)
        {
            // Verificar placa única
            var existePlaca = await _context.Vehiculos.AnyAsync(v => v.Placa == vehiculoCreaDTO.Placa);
            if (existePlaca)
                return BadRequest($"Ya existe un vehículo con la placa '{vehiculoCreaDTO.Placa}'.");

            // Verificar existencia de Modelo
            var modeloExiste = await _context.Modelos.AnyAsync(m => m.Id == vehiculoCreaDTO.ModeloId);
            if (!modeloExiste)
                return BadRequest($"El modelo con Id {vehiculoCreaDTO.ModeloId} no existe.");

            // Verificar existencia de Color
            var colorExiste = await _context.Colores.AnyAsync(c => c.Id == vehiculoCreaDTO.ColorId);
            if (!colorExiste)
                return BadRequest($"El color con Id {vehiculoCreaDTO.ColorId} no existe.");

            // Verificar existencia de AnioVehiculo
            var anioExiste = await _context.AnioVehiculos.AnyAsync(a => a.Id == vehiculoCreaDTO.AnioVehiculoId);
            if (!anioExiste)
                return BadRequest($"El año de vehículo con Id {vehiculoCreaDTO.AnioVehiculoId} no existe.");

            // Verificar existencia de Conductor
            var conductorExiste = await _context.Conductores.AnyAsync(c => c.Id == vehiculoCreaDTO.ConductorId);
            if (!conductorExiste)
                return BadRequest($"El conductor con Id {vehiculoCreaDTO.ConductorId} no existe.");

            var vehiculo = _mapper.Map<Vehiculo>(vehiculoCreaDTO);
            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();

            var vehiculoDTO = _mapper.Map<VehiculoDTO>(vehiculo);
            return CreatedAtRoute("ObtenerVehiculo", new { id = vehiculo.Id }, vehiculoDTO);
        }

        // PUT: api/Vehiculo/5 (solo Administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(int id, VehiculoCreaDTO vehiculoCreaDTO)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
                return NotFound($"No existe el vehículo con Id {id}");

            // Si cambia la placa, verificar duplicado excluyéndose a sí mismo
            if (vehiculo.Placa != vehiculoCreaDTO.Placa)
            {
                var placaDuplicada = await _context.Vehiculos.AnyAsync(v => v.Placa == vehiculoCreaDTO.Placa && v.Id != id);
                if (placaDuplicada)
                    return BadRequest($"Ya existe otro vehículo con la placa '{vehiculoCreaDTO.Placa}'.");
            }

            // Validar existencia de FK si cambian
            if (vehiculo.ModeloId != vehiculoCreaDTO.ModeloId)
            {
                var modeloExiste = await _context.Modelos.AnyAsync(m => m.Id == vehiculoCreaDTO.ModeloId);
                if (!modeloExiste)
                    return BadRequest($"El modelo con Id {vehiculoCreaDTO.ModeloId} no existe.");
            }

            if (vehiculo.ColorId != vehiculoCreaDTO.ColorId)
            {
                var colorExiste = await _context.Colores.AnyAsync(c => c.Id == vehiculoCreaDTO.ColorId);
                if (!colorExiste)
                    return BadRequest($"El color con Id {vehiculoCreaDTO.ColorId} no existe.");
            }

            if (vehiculo.AnioVehiculoId != vehiculoCreaDTO.AnioVehiculoId)
            {
                var anioExiste = await _context.AnioVehiculos.AnyAsync(a => a.Id == vehiculoCreaDTO.AnioVehiculoId);
                if (!anioExiste)
                    return BadRequest($"El año de vehículo con Id {vehiculoCreaDTO.AnioVehiculoId} no existe.");
            }

            if (vehiculo.ConductorId != vehiculoCreaDTO.ConductorId)
            {
                var conductorExiste = await _context.Conductores.AnyAsync(c => c.Id == vehiculoCreaDTO.ConductorId);
                if (!conductorExiste)
                    return BadRequest($"El conductor con Id {vehiculoCreaDTO.ConductorId} no existe.");
            }

            _mapper.Map(vehiculoCreaDTO, vehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Vehiculo/5 (solo Administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
                return NotFound("No existe el vehículo");

            // Verificar si hay seguimientos asociados
            var tieneSeguimiento = await _context.Seguimientos.AnyAsync(s => s.VehiculoId == id);
            if (tieneSeguimiento)
                return BadRequest("No se puede eliminar el vehículo porque tiene seguimientos asociados.");

            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
