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
    public class TarifaController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public TarifaController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Tarifa (público)
        [HttpGet]
        public async Task<ActionResult<List<TarifaDTO>>> Get()
        {
            var tarifas = await _context.Tarifas.ToListAsync();
            return Ok(_mapper.Map<List<TarifaDTO>>(tarifas));
        }

        // GET: api/Tarifa/5 (público)
        [HttpGet("{id:int}", Name = "ObtenerTarifa")]
        public async Task<ActionResult<TarifaDTO>> Get(int id)
        {
            var tarifa = await _context.Tarifas.FindAsync(id);
            if (tarifa == null)
                return NotFound("No existe la tarifa");

            return Ok(_mapper.Map<TarifaDTO>(tarifa));
        }

        // POST: api/Tarifa (solo Administrador)
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<TarifaDTO>> Post(TarifaCreaDTO tarifaCreaDTO)
        {
            // Verificar que el TipoVehiculoId exista
            var tipoVehiculoExiste = await _context.TipoVehiculos.AnyAsync(tv => tv.Id == tarifaCreaDTO.TipoVehiculoId);
            if (!tipoVehiculoExiste)
                return BadRequest($"El tipo de vehículo con Id {tarifaCreaDTO.TipoVehiculoId} no existe.");

            // Verificar que no exista ya una tarifa para ese tipo de vehículo
            var existe = await _context.Tarifas.AnyAsync(t => t.TipoVehiculoId == tarifaCreaDTO.TipoVehiculoId);
            if (existe)
                return Conflict($"Ya existe una tarifa para el tipo de vehículo con Id {tarifaCreaDTO.TipoVehiculoId}.");

            var tarifa = _mapper.Map<Tarifa>(tarifaCreaDTO);
            _context.Tarifas.Add(tarifa);
            await _context.SaveChangesAsync();

            var tarifaDTO = _mapper.Map<TarifaDTO>(tarifa);
            return CreatedAtRoute("ObtenerTarifa", new { id = tarifa.Id }, tarifaDTO);
        }

        // PUT: api/Tarifa/5 (solo Administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(int id, TarifaCreaDTO tarifaCreaDTO)
        {
            var tarifa = await _context.Tarifas.FindAsync(id);
            if (tarifa == null)
                return NotFound($"No existe la tarifa con Id {id}");

            // Validar existencia del tipo de vehículo
            var tipoVehiculoExiste = await _context.TipoVehiculos.AnyAsync(tv => tv.Id == tarifaCreaDTO.TipoVehiculoId);
            if (!tipoVehiculoExiste)
                return BadRequest($"El tipo de vehículo con Id {tarifaCreaDTO.TipoVehiculoId} no existe.");

            // Si se cambia el tipo de vehículo, verificar que no haya otra tarifa para ese tipo (excepto la actual)
            if (tarifa.TipoVehiculoId != tarifaCreaDTO.TipoVehiculoId)
            {
                var otraTarifa = await _context.Tarifas.AnyAsync(t => t.TipoVehiculoId == tarifaCreaDTO.TipoVehiculoId && t.Id != id);
                if (otraTarifa)
                    return Conflict($"Ya existe una tarifa para el tipo de vehículo con Id {tarifaCreaDTO.TipoVehiculoId}.");
            }

            _mapper.Map(tarifaCreaDTO, tarifa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Tarifa/5 (solo Administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var tarifa = await _context.Tarifas.FindAsync(id);
            if (tarifa == null)
                return NotFound("No existe la tarifa");

            // Verificar si hay pedidos que usen el tipo de vehículo asociado a esta tarifa
            var tienePedido = await _context.Pedidos.AnyAsync(p => p.TipoVehiculoId == tarifa.TipoVehiculoId);
            if (tienePedido)
                return BadRequest("No se puede eliminar la tarifa porque hay pedidos asociados al tipo de vehículo.");

            _context.Tarifas.Remove(tarifa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
