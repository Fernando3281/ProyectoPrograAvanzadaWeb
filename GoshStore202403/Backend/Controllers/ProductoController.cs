using Backend.DTO;
using Backend.Services.Implementations;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        IProductoService _productoService;
        public ProductoController(IProductoService productService)
        {
            this._productoService = productService;
        }

        // GET: api/<ProductoService>
        [HttpGet]
        public ActionResult Get()
        {
            var result = _productoService.GetProductos();
            return Ok(result);
        }

        // GET api/<ProductoService>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = _productoService.GetById(id);
            return Ok(result);
        }

        // POST api/<ProductoService>
        [HttpPost]
        public void Post([FromBody] ProductoDTO productoDTO)
        {
            _productoService.Add(productoDTO);
        }

        // PUT api/<ProductoService>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductoDTO productoDTO)
        {
            /*_productoService.Update(productoDTO);*/

            var productoExistente = _productoService.GetById(id);

            if (productoExistente == null)
            {
                return NotFound("El producto no fue encontrado.");
            }
            try
            {
                _productoService.Update(productoDTO);
                return Ok(productoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al editar el producto: {ex.Message}");
            }


        }

        // DELETE api/<ProductoService>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            /*_productoService.Delete(id);*/

            ProductoDTO producto= new ProductoDTO
            {
                IdProducto = id
            };
            _productoService.Delete(producto);
        }
    }
}
