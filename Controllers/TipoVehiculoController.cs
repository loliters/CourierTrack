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
    public class TipoVehiculoController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public TipoVehiculoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/TipoVehiculo (público)
        [HttpGet]
        public async Task<ActionResult<List<TipoVehiculoDTO>>> Get()
        {
            var tipos = await _context.TipoVehiculos.ToListAsync();
            return Ok(_mapper.Map<List<TipoVehiculoDTO>>(tipos));
        }

        // GET: api/TipoVehiculo/5 (público)
        [HttpGet("{id:int}", Name = "ObtenerTipoVehiculo")]
        public async Task<ActionResult<TipoVehiculoDTO>> Get(int id)
        {
            var tipo = await _context.TipoVehiculos.FindAsync(id);
            if (tipo == null)
                return NotFound("No existe el tipo de vehículo");

            return Ok(_mapper.Map<TipoVehiculoDTO>(tipo));
        }
        // POST: api/TipoVehiculo // Administrador
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<TipoVehiculoDTO>> Post(TipoVehiculoCreaDTO tipoVehiculoCreaDTO)
        {
            // Verificar duplicado (nombre único)
            var existe = await _context.TipoVehiculos.AnyAsync(x => x.Nombre == tipoVehiculoCreaDTO.Nombre);
            if (existe)
                return BadRequest($"Ya existe un tipo de vehículo con el nombre '{tipoVehiculoCreaDTO.Nombre}'.");

            var tipo = _mapper.Map<TipoVehiculo>(tipoVehiculoCreaDTO);
            _context.TipoVehiculos.Add(tipo);
            await _context.SaveChangesAsync();

            var tipoDTO = _mapper.Map<TipoVehiculoDTO>(tipo);
            return CreatedAtRoute("ObtenerTipoVehiculo", new { id = tipo.Id }, tipoDTO);
        }

        // PUT: api/TipoVehiculo/5 // Administrador
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(int id, TipoVehiculoCreaDTO tipoVehiculoCreaDTO)
        {
            var tipo = await _context.TipoVehiculos.FindAsync(id);
            if (tipo == null)
                return NotFound($"No existe el tipo de vehículo con Id {id}");

            // Verificar duplicado excluyendo el propio registro
            var duplicado = await _context.TipoVehiculos
                .AnyAsync(x => x.Nombre == tipoVehiculoCreaDTO.Nombre && x.Id != id);
            if (duplicado)
                return Conflict("Ya existe otro tipo de vehículo con ese nombre.");

            _mapper.Map(tipoVehiculoCreaDTO, tipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/TipoVehiculo/5 // Administrador
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var tipo = await _context.TipoVehiculos.FindAsync(id);
            if (tipo == null)
                return NotFound("No existe el tipo de vehículo");

            // Verificar si hay pedidos que usan este tipo de vehículo
            var tienePedido = await _context.Pedidos.AnyAsync(p => p.TipoVehiculoId == id);
            if (tienePedido)
                return BadRequest("No se puede eliminar el tipo de vehículo porque hay pedidos asociados.");

            _context.TipoVehiculos.Remove(tipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
