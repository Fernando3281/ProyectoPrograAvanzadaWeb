using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FrontEnd.Controllers
{
    public class UsuarioController : Controller
    {
        IUsuarioHelper _usuarioHelper;

        public UsuarioController(IUsuarioHelper usuarioHelper)
        {
            _usuarioHelper = usuarioHelper;
        }

        // GET: UsuarioController
        public ActionResult Index()
        {
            var lista = _usuarioHelper.GetUsuarios();
            return View(lista);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            var usuario = _usuarioHelper.GetUsuario(id);
            return View(usuario);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioViewModel usuario)
        {
            try
            {
                _usuarioHelper.Add(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al crear al usuario: " + ex.Message);
                return View(usuario);
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            var usuario = _usuarioHelper.GetUsuario(id);
            return View(usuario);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _usuarioHelper.Update(usuario.IdUsuario, usuario);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al actualizar el usuario: " + ex.Message);
                }
            }

            return View(usuario);
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            var usuario = _usuarioHelper.GetUsuario(id);
            return View(usuario);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UsuarioViewModel usuario)
        {
            try
            {
                _usuarioHelper.Delete(usuario.IdUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("Perfil")]
        public IActionResult Perfil()
        {
            // Se obtiene el id del usuario actual por medio del metodo ObtenerIdUsuarioActual aunque no se encuentra funcional
            int userId = ObtenerIdUsuarioActual();
            var usuario = _usuarioHelper.GetUsuario(userId);
            return View(usuario);
        }

        private int ObtenerIdUsuarioActual()
        {
            // es para obtener el id del usuario actual (corregir)
            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
                if (userIdClaim != null)
                {
                    return int.Parse(userIdClaim.Value);
                }
            }
            throw new Exception("No se pudo obtener el id del usuario actual.");
        }
    }
}
