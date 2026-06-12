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
    public class SeguimientoController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public SeguimientoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Seguimiento (público)
        [HttpGet]
        public async Task<ActionResult<List<SeguimientoDTO>>> Get()
        {
            var seguimientos = await _context.Seguimientos.ToListAsync();
            return Ok(_mapper.Map<List<SeguimientoDTO>>(seguimientos));
        }

        // GET: api/Seguimiento/5 (público)
        [HttpGet("{id:int}", Name = "ObtenerSeguimiento")]
        public async Task<ActionResult<SeguimientoDTO>> Get(int id)
        {
            var seguimiento = await _context.Seguimientos.FindAsync(id);
            if (seguimiento == null)
                return NotFound("No existe el seguimiento");

            return Ok(_mapper.Map<SeguimientoDTO>(seguimiento));
        }

        // POST: api/Seguimiento (solo Administrador)
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<SeguimientoDTO>> Post(SeguimientoCreaDTO seguimientoCreaDTO)
        {
            // Validar existencia de Pedido
            var pedidoExiste = await _context.Pedidos.AnyAsync(p => p.Id == seguimientoCreaDTO.PedidoId);
            if (!pedidoExiste)
                return BadRequest($"El pedido con Id {seguimientoCreaDTO.PedidoId} no existe.");

            // Validar existencia de Conductor
            var conductorExiste = await _context.Conductores.AnyAsync(c => c.Id == seguimientoCreaDTO.ConductorId);
            if (!conductorExiste)
                return BadRequest($"El conductor con Id {seguimientoCreaDTO.ConductorId} no existe.");

            // Validar existencia de Vehiculo
            var vehiculoExiste = await _context.Vehiculos.AnyAsync(v => v.Id == seguimientoCreaDTO.VehiculoId);
            if (!vehiculoExiste)
                return BadRequest($"El vehículo con Id {seguimientoCreaDTO.VehiculoId} no existe.");

            // Validar existencia de Ubicacion
            var ubicacionExiste = await _context.Ubicaciones.AnyAsync(u => u.Id == seguimientoCreaDTO.UbicacionId);
            if (!ubicacionExiste)
                return BadRequest($"La ubicación con Id {seguimientoCreaDTO.UbicacionId} no existe.");

            var seguimiento = _mapper.Map<Seguimiento>(seguimientoCreaDTO);
            _context.Seguimientos.Add(seguimiento);
            await _context.SaveChangesAsync();

            var seguimientoDTO = _mapper.Map<SeguimientoDTO>(seguimiento);
            return CreatedAtRoute("ObtenerSeguimiento", new { id = seguimiento.Id }, seguimientoDTO);
        }

        // PUT: api/Seguimiento/5 (solo Administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(int id, SeguimientoCreaDTO seguimientoCreaDTO)
        {
            var seguimiento = await _context.Seguimientos.FindAsync(id);
            if (seguimiento == null)
                return NotFound($"No existe el seguimiento con Id {id}");

            // Validar existencia de FK si cambian
            if (seguimiento.PedidoId != seguimientoCreaDTO.PedidoId)
            {
                var pedidoExiste = await _context.Pedidos.AnyAsync(p => p.Id == seguimientoCreaDTO.PedidoId);
                if (!pedidoExiste)
                    return BadRequest($"El pedido con Id {seguimientoCreaDTO.PedidoId} no existe.");
            }

            if (seguimiento.ConductorId != seguimientoCreaDTO.ConductorId)
            {
                var conductorExiste = await _context.Conductores.AnyAsync(c => c.Id == seguimientoCreaDTO.ConductorId);
                if (!conductorExiste)
                    return BadRequest($"El conductor con Id {seguimientoCreaDTO.ConductorId} no existe.");
            }

            if (seguimiento.VehiculoId != seguimientoCreaDTO.VehiculoId)
            {
                var vehiculoExiste = await _context.Vehiculos.AnyAsync(v => v.Id == seguimientoCreaDTO.VehiculoId);
                if (!vehiculoExiste)
                    return BadRequest($"El vehículo con Id {seguimientoCreaDTO.VehiculoId} no existe.");
            }

            if (seguimiento.UbicacionId != seguimientoCreaDTO.UbicacionId)
            {
                var ubicacionExiste = await _context.Ubicaciones.AnyAsync(u => u.Id == seguimientoCreaDTO.UbicacionId);
                if (!ubicacionExiste)
                    return BadRequest($"La ubicación con Id {seguimientoCreaDTO.UbicacionId} no existe.");
            }

            _mapper.Map(seguimientoCreaDTO, seguimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Seguimiento/5 (solo Administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var seguimiento = await _context.Seguimientos.FindAsync(id);
            if (seguimiento == null)
                return NotFound("No existe el seguimiento");

            // Verificar si hay historial de ubicaciones asociado
            var tieneHistorial = await _context.HistorialUbicaciones.AnyAsync(h => h.SeguimientoId == id);
            if (tieneHistorial)
                return BadRequest("No se puede eliminar el seguimiento porque tiene registros en el historial de ubicaciones.");

            _context.Seguimientos.Remove(seguimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
