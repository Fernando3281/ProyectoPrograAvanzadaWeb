using Backend.Services.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Backend.DTO;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // GET: api/Pedido
        [HttpGet]
        public IEnumerable<PedidoDTO> Get()
        {
            return _pedidoService.Obtener();
        }

        // GET api/Pedido/5
        [HttpGet("{id}")]
        public ActionResult<PedidoDTO> Get(int id)
        {
            var pedido = _pedidoService.Obtener(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return pedido;
        }

        // POST api/Pedido
        [HttpPost]
        public ActionResult Post([FromBody] PedidoDTO pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = _pedidoService.Agregar(pedido);
            if (!resultado)
            {
                return BadRequest();
            }
            return Ok(pedido);
        }

        // PUT api/Pedido/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] PedidoDTO pedido)
        {
            if (id != pedido.IdPedido)
            {
                return BadRequest();
            }

            var pedidoExistente = _pedidoService.Obtener(id);
            if (pedidoExistente == null)
            {
                return NotFound();
            }

            var resultado = _pedidoService.Editar(pedido);
            if (!resultado)
            {
                return BadRequest();
            }
            return Ok(pedido);
        }

        // DELETE api/Pedido/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var pedido = _pedidoService.Obtener(id);
            if (pedido == null)
            {
                return NotFound();
            }

            var resultado = _pedidoService.Eliminar(pedido);
            if (!resultado)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}