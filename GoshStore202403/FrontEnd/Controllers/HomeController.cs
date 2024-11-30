using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FrontEnd.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        IProductoHelper _productoHelper;
        ICategoriaHelper _categoriaHelper;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IProductoHelper productoHelper, ICategoriaHelper categoriaHelper)
        {
            this._productoHelper = productoHelper;
            this._categoriaHelper = categoriaHelper;
            _logger = logger;
        }

        public ActionResult Index()
        {
            var productos = _productoHelper.GetAll();
            foreach (var item in productos)
            {
                item.categoryName = _categoriaHelper.GetById((int)item.CategoriaId).NombreCategoria;
            }
                return View(productos);
        }

        public IActionResult Contacto()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
