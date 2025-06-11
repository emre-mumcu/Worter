using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0

namespace Wörter.AppLib.Configuration.Ext;

public static class Authentication
{
    public static IServiceCollection _AddAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.Name = AppConstants.Auth_Cookie_Name;
                options.LoginPath = AppConstants.Auth_Cookie_LoginPath;
                options.LogoutPath = AppConstants.Auth_Cookie_LogoutPath;
                options.AccessDeniedPath = AppConstants.Auth_Cookie_AccessDeniedPath;
                options.ClaimsIssuer = AppConstants.Auth_Cookie_ClaimsIssuer;
                options.ReturnUrlParameter = AppConstants.Auth_Cookie_ReturnUrlParameter;
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true; // false: xss vulnerability !!!
                options.ExpireTimeSpan = TimeSpan.FromMinutes(AppConstants.Auth_Cookie_ExpireTimeSpan);
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Validate();
                options.EventsType = typeof(CustomCookieAuthenticationEvents);
            })
        ;

        services.AddScoped<CustomCookieAuthenticationEvents>();

        return services;
    }

    public static IApplicationBuilder _UseAuthentication(this WebApplication app)
    {
        app.UseAuthentication();

        return app;
    }
}

public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
{
    /// <summary>
    /// Once the authentication cookie is created, the cookie is the single source of identity.
    /// The ValidatePrincipal event can be used to intercept and override validation of the cookie identity. 
    /// Validating the authentication cookie on every request mitigates the risk of revoked users accessing the app.
    /// </summary>
    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        var userPrincipal = context.Principal;

        if (!context.Principal.Identity.IsAuthenticated)
        {
            context.RejectPrincipal();
            await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
