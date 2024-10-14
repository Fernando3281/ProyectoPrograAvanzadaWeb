using Backend.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Backend.DTO;

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
        public IEnumerable<UsuarioDTO> Get()
        {
            return _usuarioService.Obtener();
        }

        // GET api/Usuario/5
        [HttpGet("{id}")]
        public UsuarioDTO Get(int id)
        {
           return _usuarioService.Obtener(id);
        }

        // POST api/Usuario
        [HttpPost]
        public ActionResult Post([FromBody] UsuarioDTO usuario)
        {
            _usuarioService.Agregar(usuario);
            return Ok(usuario);
        }

        // PUT api/Usuario/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UsuarioDTO usuario)
        {
            var usuarioExistente = _usuarioService.Obtener(id);

            if (usuarioExistente == null)
            {
                return NotFound("El usuario no fue encontrado.");
            }
            try
            {
                _usuarioService.Editar(usuario);
                return Ok(usuario); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al editar el usuario: {ex.Message}");
            }
        }

        // DELETE api/Usuario/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            UsuarioDTO usuario = new UsuarioDTO
            {
                IdUsuario = id
            };
            _usuarioService.Eliminar(usuario);
        }
    }
}