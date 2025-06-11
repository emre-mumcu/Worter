using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Wörter.AppLib;
using Wörter.AppLib.Configuration.Ext;
using System.Security.Claims;

namespace Wörter.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            HttpContext.Session.RemoveKey(AppConstants.SessionKeyLogin);

            HttpContext.Session.RemoveKey(AppConstants.SessionKeyLoginUser);

            HttpContext.User = new ClaimsPrincipal();

            HttpContext.Session.Clear();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var session_cookie = HttpContext.Request.Cookies[AppConstants.Session_Cookie_Name];
            if (session_cookie != null)
            {
                var options = new CookieOptions { Expires = DateTime.Now.AddDays(-1) };
                HttpContext.Response.Cookies.Append(AppConstants.Session_Cookie_Name, session_cookie, options);
            }

            var auth_cookie = HttpContext.Request.Cookies[AppConstants.Auth_Cookie_Name];
            if (auth_cookie != null)
            {
                var options = new CookieOptions { Expires = DateTime.Now.AddDays(-1) };
                HttpContext.Response.Cookies.Append(AppConstants.Session_Cookie_Name, auth_cookie, options);
            }

            return new RedirectResult("~/Home/Index");
        }
    }
}
