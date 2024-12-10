using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace FrontEnd.Controllers
{
    public class CarritoController : Controller
    {
        ICarritoHelper _carritoHelper;

        public CarritoController(ICarritoHelper carritoHelper)
        {
            _carritoHelper = carritoHelper;
        }

        public async Task<IActionResult> Index()
        {
            string usuarioId = ObtenerUsuarioId(); 
            var carrito = await _carritoHelper.ObtenerCarritoAsync(usuarioId);
            return View(carrito); 
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(CarritoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Llamando al helper para agregar al carrito");
                await _carritoHelper.AgregarAlCarritoAsync(model);
                Console.WriteLine("El helper terminó su ejecución");
                return RedirectToAction("Index");
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Error: {error.ErrorMessage}");
            }

            return View("Error"); 
        }

        // Acción para eliminar un producto del carrito
        [HttpPost]
        public async Task<IActionResult> Eliminar(int idCarrito, int idProducto)
        {
            // Llama al helper con el método DELETE
            await _carritoHelper.EliminarDelCarritoAsync(idCarrito, idProducto);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Vaciar()
        {
            string usuarioId = ObtenerUsuarioId();
            await _carritoHelper.VaciarCarritoAsync(usuarioId);
            return RedirectToAction("Index");
        }

        private string ObtenerUsuarioId()
        {
            var usuarioIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (usuarioIdClaim == null)
            {
                throw new UnauthorizedAccessException("El claim 'UsuarioId' no está presente en el token.");
            }

            return usuarioIdClaim.Value;
        }
    }
}
