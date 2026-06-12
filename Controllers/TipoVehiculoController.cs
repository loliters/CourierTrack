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
    public class TipoVehiculoController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public TipoVehiculoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TipoVehiculo (cualquier usuario autenticado)
        [HttpGet]
        public async Task<ActionResult<List<TipoVehiculoDTO>>> Get()
        {
            var tipos = await _context.TipoVehiculos.ToListAsync();
            return Ok(_mapper.Map<List<TipoVehiculoDTO>>(tipos));
        }

        // GET: api/TipoVehiculo/5
        [HttpGet("{id:int}", Name = "ObtenerTipoVehiculo")]
        public async Task<ActionResult<TipoVehiculoDTO>> Get(int id)
        {
            var tipo = await _context.TipoVehiculos.FindAsync(id);
            if (tipo == null)
                return NotFound("No existe el tipo de vehículo");
            return Ok(_mapper.Map<TipoVehiculoDTO>(tipo));
        }

        // POST: api/TipoVehiculo (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<TipoVehiculoDTO>> Post(TipoVehiculoCreaDTO dto)
        {
            // Verificar duplicado (nombre único)
            if (await _context.TipoVehiculos.AnyAsync(tv => tv.Nombre == dto.Nombre))
                return BadRequest($"Ya existe un tipo de vehículo con el nombre '{dto.Nombre}'.");

            var tipo = _mapper.Map<TipoVehiculo>(dto);
            _context.TipoVehiculos.Add(tipo);
            await _context.SaveChangesAsync();

            var tipoDTO = _mapper.Map<TipoVehiculoDTO>(tipo);
            return CreatedAtRoute("ObtenerTipoVehiculo", new { id = tipo.Id }, tipoDTO);
        }

        // PUT: api/TipoVehiculo/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, TipoVehiculoCreaDTO dto)
        {
            var tipo = await _context.TipoVehiculos.FindAsync(id);
            if (tipo == null)
                return NotFound($"No existe el tipo de vehículo con Id {id}");

            // Verificar duplicado excluyendo el propio registro
            if (await _context.TipoVehiculos.AnyAsync(tv => tv.Nombre == dto.Nombre && tv.Id != id))
                return Conflict("Ya existe otro tipo de vehículo con ese nombre.");

            _mapper.Map(dto, tipo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/TipoVehiculo/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var tipo = await _context.TipoVehiculos.FindAsync(id);
            if (tipo == null)
                return NotFound("No existe el tipo de vehículo");

            // Verificar si hay pedidos que usan este tipo de vehículo
            if (await _context.Pedidos.AnyAsync(p => p.TipoVehiculoId == id))
                return BadRequest("No se puede eliminar el tipo de vehículo porque hay pedidos asociados.");

            _context.TipoVehiculos.Remove(tipo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}