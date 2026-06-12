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
    public class TipoLicenciaController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public TipoLicenciaController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TipoLicencia (cualquier usuario autenticado)
        [HttpGet]
        public async Task<ActionResult<List<TipoLicenciaDTO>>> Get()
        {
            var tipos = await _context.TipoLicencias.ToListAsync();
            return Ok(_mapper.Map<List<TipoLicenciaDTO>>(tipos));
        }

        // GET: api/TipoLicencia/5
        [HttpGet("{id:int}", Name = "ObtenerTipoLicencia")]
        public async Task<ActionResult<TipoLicenciaDTO>> Get(int id)
        {
            var tipo = await _context.TipoLicencias.FindAsync(id);
            if (tipo == null)
                return NotFound("No existe el tipo de licencia");
            return Ok(_mapper.Map<TipoLicenciaDTO>(tipo));
        }

        // POST: api/TipoLicencia (solo administrador)
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult<TipoLicenciaDTO>> Post(TipoLicenciaCreaDTO dto)
        {
            // Verificar duplicado (categoría única)
            if (await _context.TipoLicencias.AnyAsync(tl => tl.Categoria == dto.Categoria))
                return BadRequest($"Ya existe un tipo de licencia con la categoría '{dto.Categoria}'.");

            var tipoLicencia = _mapper.Map<TipoLicencia>(dto);
            _context.TipoLicencias.Add(tipoLicencia);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<TipoLicenciaDTO>(tipoLicencia);
            return CreatedAtRoute("ObtenerTipoLicencia", new { id = tipoLicencia.Id }, resultDto);
        }

        // PUT: api/TipoLicencia/5 (solo administrador)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Put(int id, TipoLicenciaCreaDTO dto)
        {
            var tipoLicencia = await _context.TipoLicencias.FindAsync(id);
            if (tipoLicencia == null)
                return NotFound($"No existe el tipo de licencia con Id {id}");

            // Verificar duplicado excluyendo el propio registro
            if (await _context.TipoLicencias.AnyAsync(tl => tl.Categoria == dto.Categoria && tl.Id != id))
                return Conflict("Ya existe otro tipo de licencia con esa categoría.");

            _mapper.Map(dto, tipoLicencia);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/TipoLicencia/5 (solo administrador)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var tipoLicencia = await _context.TipoLicencias.FindAsync(id);
            if (tipoLicencia == null)
                return NotFound("No existe el tipo de licencia");

            // Verificar si hay conductores que usan esta licencia
            if (await _context.Conductores.AnyAsync(c => c.TipoLicenciaId == id))
                return BadRequest("No se puede eliminar el tipo de licencia porque hay conductores asociados.");

            _context.TipoLicencias.Remove(tipoLicencia);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}