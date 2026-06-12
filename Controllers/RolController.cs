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
    public class RolController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public RolController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/rol
        [HttpGet]
        public async Task<ActionResult<List<RolDTO>>> Get()
        {
            var roles = await _context.Roles.ToListAsync();

            return _mapper.Map<List<RolDTO>>(roles);
        }

        // GET: api/rol/5
        [HttpGet("{id:int}", Name = "ObtenerRol")]
        public async Task<ActionResult<RolDTO>> Get(int id)
        {
            var rol = await _context.Roles
                .FirstOrDefaultAsync(x => x.Id == id);

            if (rol == null)
                return NotFound("Rol no encontrado.");

            return _mapper.Map<RolDTO>(rol);
        }

        // POST: api/rol
        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Post([FromBody] RolCreaDTO rolCreaDTO)
        {
            var existe = await _context.Roles
                .AnyAsync(x => x.Nombre == rolCreaDTO.Nombre);

            if (existe)
            {
                return BadRequest(
                    $"Ya existe un rol con el nombre {rolCreaDTO.Nombre}");
            }

            var rol = _mapper.Map<Rol>(rolCreaDTO);

            _context.Add(rol);
            await _context.SaveChangesAsync();

            var rolDTO = _mapper.Map<RolDTO>(rol);

            return CreatedAtRoute(
                "ObtenerRol",
                new { id = rol.Id },
                rolDTO);
        }

        // PUT: api/rol/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> Put(
            int id,
            RolCreaDTO rolCreaDTO)
        {
            var existeRol = await _context.Roles
                .AnyAsync(x => x.Id == id);

            if (!existeRol)
                return NotFound("No existe el rol.");

            var rol = _mapper.Map<Rol>(rolCreaDTO);
            rol.Id = id;

            _context.Update(rol);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/rol/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var rol = await _context.Roles.FindAsync(id);

            if (rol == null)
                return NotFound("El rol no existe.");

            _context.Roles.Remove(rol);

            await _context.SaveChangesAsync();

            return Ok("Rol eliminado correctamente.");
        }
    }
}