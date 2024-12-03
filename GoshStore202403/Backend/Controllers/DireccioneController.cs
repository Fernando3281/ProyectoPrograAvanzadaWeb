using Backend.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Backend.DTO;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccioneController : ControllerBase
    {
        private readonly IDireccioneService _direccioneService;

        public DireccioneController(IDireccioneService usuarioService)
        {
            _direccioneService = usuarioService;
        }

        // GET: api/Direccion
        [HttpGet]
        public IEnumerable<DireccioneDTO> Get()
        {
            return _direccioneService.GetDireccione();
        }

        // GET api/Direccion/5
        [HttpGet("{id}")]
        public DireccioneDTO Get(int id)
        {
           return _direccioneService.GetByID(id);
        }

        // POST api/Direccion
        [HttpPost]
        public ActionResult Post([FromBody] DireccioneDTO direccione)
        {
            _direccioneService.Add(direccione);
            return Ok(direccione);
        }

        // PUT api/Direccion/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DireccioneDTO direccion)
        {
            var direccionExistente = _direccioneService.GetByID(id);

            if (direccionExistente == null)
            {
                return NotFound("La dirección no fue encontrada.");
            }
            try
            {
                _direccioneService.Update(direccion);
                return Ok(direccion); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al editar la dirección: {ex.Message}");
            }
        }

        // DELETE api/Direccion/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            DireccioneDTO direccione = new DireccioneDTO
            {
                IdDireccion = id
            };
            _direccioneService.Delete(direccione);
        }
    }
}