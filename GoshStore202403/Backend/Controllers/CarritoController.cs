using Backend.DTO;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requiere autenticación
    public class CarritoController : ControllerBase
    {
        private readonly ICarritoService _carritoService;

        public CarritoController(ICarritoService carritoService)
        {
            _carritoService = carritoService;
        }

        // GET: api/Carrito
        [HttpGet]
        public async Task<IActionResult> VerCarrito()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("No se pudo identificar al usuario.");
            }

            var carrito = await _carritoService.VerCarritoAsync(userId);
            if (carrito == null || !carrito.Any())
            {
                return NotFound("El carrito está vacío o no se pudo encontrar.");
            }

            return Ok(carrito);
        }


        // POST: api/Carrito/Add
        [HttpPost("Add")]
        public async Task<IActionResult> AgregarProducto([FromBody] CarritoDTO carrito)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("No se pudo identificar al usuario.");
            }

            carrito.IdUsuario = userId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var result = await _carritoService.AgregarProductoAsync(carrito);

            if (!result)
            {
                return BadRequest("No se pudo agregar el producto al carrito.");
            }

            return Ok("Producto agregado al carrito exitosamente.");
        }

        // DELETE: api/Carrito/Remove/{idProducto}
        [HttpDelete("Remove/{idProducto}")]
        public async Task<IActionResult> EliminarProducto(int idProducto)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioId))
            {
                return Unauthorized("No se pudo identificar al usuario.");
            }

            var resultado = await _carritoService.EliminarProductoAsync(idProducto, usuarioId);
            if (!resultado)
            {
                return NotFound("El producto no se encontró en el carrito o ya fue eliminado.");
            }

            return Ok("Producto eliminado del carrito exitosamente.");
        }


        // POST: api/Carrito/Clear
        [HttpPost("Clear")]
        public async Task<IActionResult> LimpiarCarrito()
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(usuarioId))
            {
                return Unauthorized("No se pudo identificar al usuario.");
            }

            var resultado = await _carritoService.LimpiarCarritoAsync(usuarioId);
            if (!resultado)
            {
                return BadRequest("No se pudo limpiar el carrito.");
            }

            return Ok("Carrito limpiado con éxito.");
        }
    }
}
