using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Wörter.AppLib;
using Wörter.AppLib.Configuration.Ext;
using System.Security.Claims;

namespace Wörter.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public async Task<IActionResult> OnGet(string AppReturn)
        {
            // return Page();

            AuthenticationProperties prop = new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
            };

            IEnumerable<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, "USER-ID"),
                new Claim(ClaimTypes.Name, "APP-USER"),
                new Claim(ClaimTypes.Role, "USER")
            };

            ClaimsIdentity ci = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal cp = new ClaimsPrincipal(ci);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, cp, prop);

            HttpContext.Session.SetKey<bool>(AppConstants.SessionKeyLogin, true);

            HttpContext.Session.SetKey<string>(AppConstants.SessionKeyLoginUser, "USER-ID");

            if (string.IsNullOrEmpty(AppReturn)) return RedirectToAction("Index", "Home");

            else return LocalRedirect(AppReturn); 
        }
    }
}
