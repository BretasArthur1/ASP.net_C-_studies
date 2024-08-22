
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using asp.net_mvc.Models;
using Microsoft.AspNetCore.Authentication;

namespace asp.net_mvc.Controllers
{
    public class AccessController : Controller
    {     
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(Login model){

            if(model.Email == "user@example.com" && model.Password == "123"){

                List<Claim> claims =
                [
                    new(ClaimTypes.NameIdentifier, model.Email),
                    new("OtherProprieties", "Role")
                ];

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties proprieties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = model.KeepLoggedIn   
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), proprieties);

                return RedirectToAction("Index", "Home");
            }
            
            ViewData["ValidateMessage"] = "User not found";
            return View();
        }
    }
}



    