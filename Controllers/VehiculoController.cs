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
    public class VehiculoController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public VehiculoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Vehiculo (cualquier usuario autenticado)
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

        // POST: api/Vehiculo (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<VehiculoDTO>> Post(VehiculoCreaDTO dto)
        {
            // Verificar placa única
            if (await _context.Vehiculos.AnyAsync(v => v.Placa == dto.Placa))
                return BadRequest($"Ya existe un vehículo con la placa '{dto.Placa}'.");

            // Verificar existencia de FK
            if (!await _context.Modelos.AnyAsync(m => m.Id == dto.ModeloId))
                return BadRequest($"El modelo con Id {dto.ModeloId} no existe.");
            if (!await _context.Colores.AnyAsync(c => c.Id == dto.ColorId))
                return BadRequest($"El color con Id {dto.ColorId} no existe.");
            if (!await _context.AnioVehiculos.AnyAsync(a => a.Id == dto.AnioVehiculoId))
                return BadRequest($"El año de vehículo con Id {dto.AnioVehiculoId} no existe.");
            if (!await _context.Conductores.AnyAsync(c => c.Id == dto.ConductorId))
                return BadRequest($"El conductor con Id {dto.ConductorId} no existe.");

            var vehiculo = _mapper.Map<Vehiculo>(dto);
            _context.Vehiculos.Add(vehiculo);
            await _context.SaveChangesAsync();

            var vehiculoDTO = _mapper.Map<VehiculoDTO>(vehiculo);
            return CreatedAtRoute("ObtenerVehiculo", new { id = vehiculo.Id }, vehiculoDTO);
        }

        // PUT: api/Vehiculo/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, VehiculoCreaDTO dto)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
                return NotFound($"No existe el vehículo con Id {id}");

            // Si cambia la placa, verificar duplicado
            if (vehiculo.Placa != dto.Placa && await _context.Vehiculos.AnyAsync(v => v.Placa == dto.Placa && v.Id != id))
                return BadRequest($"Ya existe otro vehículo con la placa '{dto.Placa}'.");

            // Validar FK si cambian
            if (vehiculo.ModeloId != dto.ModeloId && !await _context.Modelos.AnyAsync(m => m.Id == dto.ModeloId))
                return BadRequest($"El modelo con Id {dto.ModeloId} no existe.");
            if (vehiculo.ColorId != dto.ColorId && !await _context.Colores.AnyAsync(c => c.Id == dto.ColorId))
                return BadRequest($"El color con Id {dto.ColorId} no existe.");
            if (vehiculo.AnioVehiculoId != dto.AnioVehiculoId && !await _context.AnioVehiculos.AnyAsync(a => a.Id == dto.AnioVehiculoId))
                return BadRequest($"El año de vehículo con Id {dto.AnioVehiculoId} no existe.");
            if (vehiculo.ConductorId != dto.ConductorId && !await _context.Conductores.AnyAsync(c => c.Id == dto.ConductorId))
                return BadRequest($"El conductor con Id {dto.ConductorId} no existe.");

            _mapper.Map(dto, vehiculo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Vehiculo/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
                return NotFound("No existe el vehículo");

            // Verificar dependencias (seguimientos)
            if (await _context.Seguimientos.AnyAsync(s => s.VehiculoId == id))
                return BadRequest("No se puede eliminar el vehículo porque tiene seguimientos asociados.");

            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}