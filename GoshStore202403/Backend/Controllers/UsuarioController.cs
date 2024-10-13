using Backend.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/Usuario
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> Get()
        {
            return Ok(_usuarioService.Obtener());
        }

        // GET api/Usuario/5
        [HttpGet("{id}")]
        public ActionResult<Usuario> Get(int id)
        {
            var usuario = _usuarioService.Obtener(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        // POST api/Usuario
        [HttpPost]
        public ActionResult<Usuario> Post([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_usuarioService.Agregar(usuario))
            {
                return CreatedAtAction(nameof(Get), new { id = usuario.IdUsuario }, usuario);
            }

            return BadRequest("No se pudo agregar el usuario");
        }

        // PUT api/Usuario/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_usuarioService.Editar(usuario))
            {
                return NoContent();
            }

            return BadRequest("No se pudo actualizar el usuario");
        }

        // DELETE api/Usuario/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = _usuarioService.Obtener(id);
            if (usuario == null)
            {
                return NotFound();
            }

            if (_usuarioService.Eliminar(usuario))
            {
                return NoContent();
            }

            return BadRequest("No se pudo eliminar el usuario");
        }
    }
}