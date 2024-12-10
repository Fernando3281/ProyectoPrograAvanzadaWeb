using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace FrontEnd.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoHelper _pedidoHelper;
        private readonly IDetallePedidoHelper _detallePedidoHelper;

        public PedidoController(IPedidoHelper pedidoHelper, IDetallePedidoHelper detallePedidoHelper)
        {
            _pedidoHelper = pedidoHelper;
            _detallePedidoHelper = detallePedidoHelper;
        }

        // GET: PedidoController
        public ActionResult Index()
        {
            var lista = _pedidoHelper.GetPedidos();
            return View(lista);
        }

        // GET: PedidoController/Details/5
        public ActionResult Details(int id)
        {
            var pedido = _pedidoHelper.GetPedido(id);
            var detalles = _detallePedidoHelper.GetDetallesPedido();
            ViewBag.Detalles = detalles.Where(d => d.IdPedido == id).ToList();
            return View(pedido);
        }

        // GET: PedidoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PedidoViewModel pedido)
        {
            try
            {
                _pedidoHelper.Add(pedido);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al crear el pedido: " + ex.Message);
                return View(pedido);
            }
        }

        // GET: PedidoController/Edit/5
        public ActionResult Edit(int id)
        {
            var pedido = _pedidoHelper.GetPedido(id);
            return View(pedido);
        }

        // POST: PedidoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PedidoViewModel pedido)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _pedidoHelper.Update(pedido.IdPedido, pedido);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al actualizar el pedido: " + ex.Message);
                }
            }

            return View(pedido);
        }

        // GET: PedidoController/Delete/5
        public ActionResult Delete(int id)
        {
            var pedido = _pedidoHelper.GetPedido(id);
            return View(pedido);
        }

        // POST: PedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PedidoViewModel pedido)
        {
            try
            {
                _pedidoHelper.Delete(pedido.IdPedido);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       

    }
}
