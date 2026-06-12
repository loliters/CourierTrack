using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using WebAppCourierTrack.DTO;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoLicenciaController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public TipoLicenciaController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/TipoLicencia
        [HttpGet]
        public async Task<ActionResult<List<TipoLicenciaDTO>>> Get()
        {
            var tipos = await _context.TipoLicencias.ToListAsync();
            return _mapper.Map<List<TipoLicenciaDTO>>(tipos);
        }
        [HttpGet("{id:int}", Name = "ObtenerTipoLicencia")]
        public async Task<ActionResult<TipoLicenciaDTO>> Get(int id)
        {
            var tipo = await _context.TipoLicencias.FindAsync(id);
            if (tipo == null)
                return NotFound("No existe el tipo de licencia");

            return _mapper.Map<TipoLicenciaDTO>(tipo);
            //return Ok(tipoDto);
        }
        // POST: api/TipoLicencia//solo Administrador
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<TipoLicenciaDTO>> Post(TipoLicenciaCreaDTO tiplicCreadto)
        {
            /*if (!ModelState.IsValid)
                return BadRequest(ModelState);*/
            // Verificar duplicado (ya existe una categoría igual)
            var existe = await _context.TipoLicencias.AnyAsync(x => x.Categoria == tiplicCreadto.Categoria);
            if (existe)
            {
                return BadRequest($"Ya existe un tipo de licencia con ese nombre de categoría{tiplicCreadto.Categoria}");
            }
   
            var tipoLicencia = _mapper.Map<TipoLicencia>(tiplicCreadto);
            _context.TipoLicencias.Add(tipoLicencia);
            await _context.SaveChangesAsync();

            var tipoLicenciaDTO = _mapper.Map<TipoLicenciaDTO>(tipoLicencia);
            //return CreatedAtAction(nameof(Get), new { id = nuevo.Id }, resultDto);
            return CreatedAtRoute("ObtenerTipoLicencia", new {id =tipoLicencia.Id}, tipoLicenciaDTO);
        }
        // PUT//solo Administrador
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(int id, TipoLicenciaCreaDTO tipoLicenciaDTO)
        {
            /*if (!ModelState.IsValid)
                return BadRequest(ModelState);*/

            var tipoLicencia = await _context.TipoLicencias.FindAsync(id);
            if (tipoLicencia == null)
                return NotFound($"No existe el tipo de licencia con Id {id}");

            // Verificar duplicado excluyendo el propio registro
            var duplicado = await _context.TipoLicencias
                .AnyAsync(x => x.Categoria == tipoLicenciaDTO.Categoria && x.Id != id);
            if (duplicado)
                return Conflict("Ya existe otro tipo de licencia con esa categoría.");

            _mapper.Map(tipoLicenciaDTO, tipoLicencia); // Actualiza la categoría
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE//solo Administrador
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var tipoLicencia = await _context.TipoLicencias.FindAsync(id);
            if (tipoLicencia == null)
                return NotFound("No existe el tipo de licencia");

            // Verificar si hay conductores que usan esta licencia
            var tieneConductores = await _context.Conductores.AnyAsync(c => c.TipoLicenciaId == id);
            if (tieneConductores)
            {
                return BadRequest("No se puede eliminar el tipo de licencia porque hay conductores asociados.");
            }
            _context.TipoLicencias.Remove(tipoLicencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
