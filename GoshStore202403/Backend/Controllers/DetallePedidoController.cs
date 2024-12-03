using Backend.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Backend.DTO;
using FrontEnd.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidoController : ControllerBase
    {
        private readonly IDetallePedidoService _detallePedidoService;

        public DetallePedidoController(IDetallePedidoService detallePedidoService)
        {
            _detallePedidoService = detallePedidoService;
        }

        // GET: api/DetallePedido
        [HttpGet]
        public IEnumerable<DetallePedidoDTO> Get()
        {
            return _detallePedidoService.Obtener();
        }

        // GET api/DetallePedido/5
        [HttpGet("{id}")]
        public DetallePedidoDTO Get(int id)
        {
            return _detallePedidoService.Obtener(id);
        }

        // POST api/DetallePedido
        [HttpPost]
        public ActionResult Post([FromBody] DetallePedidoDTO detallePedido)
        {
            _detallePedidoService.Agregar(detallePedido);
            return Ok(detallePedido);
        }

        // PUT api/DetallePedido/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DetallePedidoDTO detallePedido)
        {
            var detallePedidoExistente = _detallePedidoService.Obtener(id);

            if (detallePedidoExistente == null)
            {
                return NotFound("El detalle del pedido no fue encontrado.");
            }
            try
            {
                _detallePedidoService.Editar(detallePedido);
                return Ok(detallePedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al editar el detalle del pedido: {ex.Message}");
            }
        }

        // DELETE api/DetallePedido/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            DetallePedidoDTO detallePedido = new DetallePedidoDTO
            {
                IdDetallePedido = id
            };
            _detallePedidoService.Eliminar(detallePedido);
        }
    }
}
