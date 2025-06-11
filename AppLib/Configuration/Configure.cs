using Wörter.AppLib.Configuration.Ext;

namespace MvcZero.AppLib.Configuration;

public static class Configure
{

    public async static Task<WebApplication> _Configure(this WebApplication app)
    {
        app._InitPipeline();

        app.UseHttpsRedirection();

        // app.UseStaticFiles();

		app.MapStaticAssets();

		Wörter.App.Instance._WebHostEnvironment = app.Services.GetRequiredService<IWebHostEnvironment>();

		app._UseSession();

        app.UseRouting();

        app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Strict });

        app._UseAuthentication();

        app._UseAuthorization();               

/*
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        }); */

		app.MapDefaultControllerRoute().WithStaticAssets();
		app.MapRazorPages();

		// inline middleware
		app.Use(async (context, next) =>
        {
            // Do work that doesn't write to the Response.    
            await next.Invoke();
            // Do logging or other work that doesn't write to the Response.    
        });

		/*
		app.Run(async (context) =>
        {              
            await context.Response.WriteAsync($"Service was unable to handle this request: {context.Request.Path}");
        });
		*/
		
        await Task.CompletedTask;

        return app;
    }
}