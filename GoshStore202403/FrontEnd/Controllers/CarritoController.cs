using FrontEnd.ApiModels;
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
            string usuarioId = ObtenerUsuarioId();
            var carrito = _carritoHelper.ObtenerCarritoAsync(usuarioId).Result;

            if (carrito == null || !carrito.Any())
            {
                ViewBag.ErrorMessage = "No hay productos en el carrito.";
                return View(new List<CarritoViewModel>());
            }

            return View(carrito);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarPedido(List<CarritoViewModel> carrito)
        {
            try
            {
                if (carrito == null || !carrito.Any())
                {
                    throw new Exception("El carrito está vacío o no se enviaron datos.");
                }

                // Generar un ID único para el pedido (puede reemplazarse con una lógica de base de datos)
                int idPedido = new Random().Next(1000, 9999); // Ejemplo de ID generado (puede ser reemplazado)

                // Generar PDF y CSV con el ID del pedido en el nombre del archivo
                var pdfPath = GenerarPDF(carrito.Select(c => new Carrito
                {
                    IdProducto = c.IdProducto,
                    Cantidad = c.Cantidad,
                    IdUsuario = c.IdUsuario
                }), idPedido);

                var csvPath = GenerarCSV(carrito.Select(c => new Carrito
                {
                    IdProducto = c.IdProducto,
                    Cantidad = c.Cantidad,
                    IdUsuario = c.IdUsuario
                }), idPedido);

                // Eliminar datos del carrito uno por uno
                string usuarioId = ObtenerUsuarioId();
                foreach (var item in carrito)
                {
                    await _carritoHelper.EliminarDelCarritoAsync(item.IdCarrito, item.IdProducto);
                }

                TempData["SuccessMessage"] = $"Pedido {idPedido} confirmado. Los archivos PDF y CSV se generaron correctamente.";
                TempData["PDFPath"] = pdfPath;
                TempData["CSVPath"] = csvPath;

                return RedirectToAction("Transacciones");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al confirmar el pedido: {ex.Message}");
                return View("Error");
            }
        }





        private string GenerarPDF(IEnumerable<Carrito> carrito, int idPedido)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Nombre del archivo basado en el idPedido
            var filePath = Path.Combine(directoryPath, $"PDF_{idPedido}.pdf");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                var document = new iTextSharp.text.Document();
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, stream);

                document.Open();

                document.Add(new iTextSharp.text.Paragraph($"Resumen del Pedido - ID {idPedido}"));
                document.Add(new iTextSharp.text.Paragraph("\n"));

                if (carrito != null && carrito.Any())
                {
                    var table = new iTextSharp.text.pdf.PdfPTable(4)
                    {
                        WidthPercentage = 100
                    };

                    table.AddCell("Producto ID");
                    table.AddCell("Cantidad");
                    table.AddCell("Precio Unitario");
                    table.AddCell("Subtotal");

                    foreach (var item in carrito)
                    {
                        table.AddCell(item.IdProducto.ToString());
                        table.AddCell(item.Cantidad.ToString());
                        table.AddCell("₡5,000.00");
                        table.AddCell((item.Cantidad * 5000).ToString("C"));
                    }

                    document.Add(table);
                }
                else
                {
                    document.Add(new iTextSharp.text.Paragraph("No hay productos en el carrito."));
                }

                document.Close();
            }

            return filePath;
        }

        private string GenerarCSV(IEnumerable<Carrito> carrito, int idPedido)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Nombre del archivo basado en el idPedido
            var filePath = Path.Combine(directoryPath, $"CSV_{idPedido}.csv");

            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Producto ID,Cantidad,Precio Unitario,Subtotal");

                foreach (var item in carrito)
                {
                    writer.WriteLine($"{item.IdProducto},{item.Cantidad},5000,{item.Cantidad * 5000}");
                }
            }

            return filePath;
        }



        public IActionResult Transacciones()
        {
           
            var files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files"))
                .Select(f => $"/files/{Path.GetFileName(f)}").ToList();

            TempData["Files"] = files;

            return View();
        }

        [HttpPost]
        public IActionResult EliminarArchivo(string fileName)
        {
            try
            {
                // Ruta completa del archivo en el servidor
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", fileName);

                // Verificar si el archivo existe
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    TempData["SuccessMessage"] = $"Archivo '{fileName}' eliminado correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = $"El archivo '{fileName}' no existe.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al eliminar el archivo: {ex.Message}";
            }

            return RedirectToAction("Transacciones");
        }

    }
}
