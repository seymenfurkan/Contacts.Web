using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Contacts.Web.Models;

namespace Contacts.Web.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Person");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ALogin login)
        {
            if (login.Username == "deneme" && login.Password == "deneme")
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,login.Username),
                    new Claim(ClaimTypes.Role , "Admin")
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = login.KeepLoggedIn,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Index", "Person");
            }

            ViewData["ValidateMessage"] = "Kullanıcı Bulunamadı";
            return View();
        }
    }
}
