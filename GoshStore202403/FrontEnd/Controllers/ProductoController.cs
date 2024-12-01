using FrontEnd.Helpers.Implementations;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace FrontEnd.Controllers
{
    public class ProductoController : Controller
    {
        IProductoHelper _productoHelper;
        ICategoriaHelper _categoriaHelper;

        public ProductoController(IProductoHelper productoHelper, ICategoriaHelper categoriaHelper)
        {
            this._productoHelper = productoHelper;
            this._categoriaHelper = categoriaHelper;
        }
        // GET: ProductoController/Index
        public ActionResult Index(string search)
        {
            var productos = _productoHelper.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                productos = productos.Where(p => p.NombreProducto.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Pasar el valor de búsqueda y si no se encontraron productos a la vista
            ViewData["SearchTerm"] = search;
            ViewData["NoProductsFound"] = productos.Count == 0;

            foreach (var item in productos)
            {
                item.categoryName = _categoriaHelper.GetById((int)item.CategoriaId).NombreCategoria;
            }

            return View(productos);
        }



        // GET: ProductoController/Details/5
        public ActionResult Details(int id)
        {
            var producto = _productoHelper.GetById(id);
            return View(producto);
        }

        // GET: ProductoController/Create
        public ActionResult Create()
        {
            ProductoViewModel producto = new ProductoViewModel();
            producto.Categorias = _categoriaHelper.GetAll();

            return View(producto);
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductoViewModel producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _productoHelper.AddProduct(producto);
                    return RedirectToAction(nameof(Index));
                }
                
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Edit/5
        public ActionResult Edit(int id)
        {
            ProductoViewModel producto = new ProductoViewModel();
            producto = _productoHelper.GetById(id);
            producto.Categorias = _categoriaHelper.GetAll();
            return View(producto);
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductoViewModel producto)
        {
            try
            {
                _productoHelper.EditProduct(producto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            var producto = _productoHelper.GetById(id);
            return View(producto);
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProductoViewModel producto)
        {
            try
            {
                _productoHelper.DeleteProduct(producto.IdProducto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Details/5
        public ActionResult InfoProducto(int id)
        {
            var producto = _productoHelper.GetById(id);
            if (producto == null)
            {
                return NotFound();
            }

            return View("DetailsProducto", producto);
        }
    }
}
