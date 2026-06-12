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
    [Authorize]
    public class MetodoPagoController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        public MetodoPagoController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/MetodoPago
        [HttpGet]
        public async Task<ActionResult<List<MetodoPagoDTO>>> Get()
        {
            var metodos = await _context.MetodoPagos.ToListAsync();
            return Ok(_mapper.Map<List<MetodoPagoDTO>>(metodos));
        }

        // GET: api/MetodoPago/5 
        [HttpGet("{id:int}", Name = "ObtenerMetodoPago")]
        public async Task<ActionResult<MetodoPagoDTO>> Get(int id)
        {
            var metodo = await _context.MetodoPagos.FindAsync(id);
            if (metodo == null)
                return NotFound("No existe el método de pago");

            return Ok(_mapper.Map<MetodoPagoDTO>(metodo));
        }

        // POST: api/MetodoPago // Administrador
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<MetodoPagoDTO>> Post(MetodoPagoCreaDTO metodoPagoCreaDTO)
        {
            // Verificar duplicado (nombre único)
            var existe = await _context.MetodoPagos.AnyAsync(x => x.Nombre == metodoPagoCreaDTO.Nombre);
            if (existe)
                return BadRequest($"Ya existe un método de pago con el nombre '{metodoPagoCreaDTO.Nombre}'.");

            var metodo = _mapper.Map<MetodoPago>(metodoPagoCreaDTO);
            _context.MetodoPagos.Add(metodo);
            await _context.SaveChangesAsync();

            var metodoDTO = _mapper.Map<MetodoPagoDTO>(metodo);
            return CreatedAtRoute("ObtenerMetodoPago", new { id = metodo.Id }, metodoDTO);
        }

        // PUT: api/MetodoPago/5 // Administrador
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, MetodoPagoCreaDTO metodoPagoCreaDTO)
        {
            var metodo = await _context.MetodoPagos.FindAsync(id);
            if (metodo == null)
                return NotFound($"No existe el método de pago con Id {id}");

            // Verificar duplicado excluyendo el propio registro
            var duplicado = await _context.MetodoPagos
                .AnyAsync(x => x.Nombre == metodoPagoCreaDTO.Nombre && x.Id != id);
            if (duplicado)
                return Conflict("Ya existe otro método de pago con ese nombre.");

            _mapper.Map(metodoPagoCreaDTO, metodo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/MetodoPago/5 // Administrador
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var metodo = await _context.MetodoPagos.FindAsync(id);
            if (metodo == null)
                return NotFound("No existe el método de pago");

            // Verificar si hay pagos asociados
            var tienePago = await _context.Pagos.AnyAsync(p => p.MetodoPagoId == id);
            if (tienePago)
                return BadRequest("No se puede eliminar el método de pago porque hay pagos asociados.");

            _context.MetodoPagos.Remove(metodo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
