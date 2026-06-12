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
    public class AnioVehiculoController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public AnioVehiculoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/AnioVehiculo (público)
        [HttpGet]
        public async Task<ActionResult<List<AnioVehiculoDTO>>> Get()
        {
            var anios = await _context.AnioVehiculos.ToListAsync();
            return Ok(_mapper.Map<List<AnioVehiculoDTO>>(anios));
        }

        // GET: api/AnioVehiculo/5 (público)
        [HttpGet("{id:int}", Name = "ObtenerAnioVehiculo")]
        public async Task<ActionResult<AnioVehiculoDTO>> Get(int id)
        {
            var anio = await _context.AnioVehiculos.FindAsync(id);
            if (anio == null)
                return NotFound("No existe el año de vehículo");

            return Ok(_mapper.Map<AnioVehiculoDTO>(anio));
        }

        // POST: api/AnioVehiculo (solo Administrador)
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<AnioVehiculoDTO>> Post(AnioVehiculoCreaDTO anioVehiculoCreaDTO)
        {
            // Verificar duplicado (año único)
            var existe = await _context.AnioVehiculos.AnyAsync(x => x.Anio == anioVehiculoCreaDTO.Anio);
            if (existe)
                return BadRequest($"Ya existe el año {anioVehiculoCreaDTO.Anio} registrado.");

            var anio = _mapper.Map<AnioVehiculo>(anioVehiculoCreaDTO);
            _context.AnioVehiculos.Add(anio);
            await _context.SaveChangesAsync();

            var anioDTO = _mapper.Map<AnioVehiculoDTO>(anio);
            return CreatedAtRoute("ObtenerAnioVehiculo", new { id = anio.Id }, anioDTO);
        }

        // PUT: api/AnioVehiculo/5 (solo Administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(int id, AnioVehiculoCreaDTO anioVehiculoCreaDTO)
        {
            var anio = await _context.AnioVehiculos.FindAsync(id);
            if (anio == null)
                return NotFound($"No existe el año de vehículo con Id {id}");

            // Verificar duplicado excluyendo el propio registro
            var duplicado = await _context.AnioVehiculos
                .AnyAsync(x => x.Anio == anioVehiculoCreaDTO.Anio && x.Id != id);
            if (duplicado)
                return Conflict("Ya existe otro registro con ese año.");

            _mapper.Map(anioVehiculoCreaDTO, anio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/AnioVehiculo/5 (solo Administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var anio = await _context.AnioVehiculos.FindAsync(id);
            if (anio == null)
                return NotFound("No existe el año de vehículo");

            // Verificar si hay vehículos asociados a este año
            var tieneVehiculo = await _context.Vehiculos.AnyAsync(v => v.AnioVehiculoId == id);
            if (tieneVehiculo)
                return BadRequest("No se puede eliminar el año porque hay vehículos asociados.");

            _context.AnioVehiculos.Remove(anio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
