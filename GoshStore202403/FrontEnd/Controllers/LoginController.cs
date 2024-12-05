using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Net.Http;

namespace FrontEnd.Controllers
{
    public class LoginController : Controller
    {

        ISecurityHelper securityHelper;

        public LoginController(ISecurityHelper securityHelper)
        {
            this.securityHelper = securityHelper;
        }

        public IActionResult Login(string ReturnUrl = "/")
        {
            UserViewModel user = new UserViewModel();
            user.ReturnUrl = ReturnUrl;
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ModelState.Remove("Email");
                    var loging = securityHelper.Login(user);

                    TokenAPI tokenAPI = loging.Token;
                    var EsValido = false;

                    if (tokenAPI != null)
                    {
                        HttpContext.Session.SetString("token", tokenAPI.Token);
                        EsValido = true;
                    }

                    if (!EsValido)
                    {
                        ViewBag.Message = "Credenciales Invalidas";
                        return View(user);
                    }


                    var claims = new List<Claim>() {
                                     new Claim(ClaimTypes.NameIdentifier, loging.Username as string),
                                         new Claim(ClaimTypes.Name, loging.Username as string)
                    };

                    foreach (var item in loging.Roles)
                    {
                        claims.Add(
                              new Claim(ClaimTypes.Role, item as string)

                            );
                    }


                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = user.RememberLogin
                    });


                    return LocalRedirect(user.ReturnUrl);
                }
                return View(user);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public IActionResult Register()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Send registration data to the API using HttpClient or your security helper
                    var registrationResult = securityHelper.Register(user);

                }

                // If we reach here, something went wrong
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                ViewBag.Message = $"An unexpected error occurred: {ex.Message}";
                return View(user);
            }
        }

    }
}
