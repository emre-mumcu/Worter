using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Wörter.AppLib.Configuration.Ext;

public static class Init
{
    public static IServiceCollection _InitServices(this IServiceCollection services)
    {
        IMvcBuilder mvcBuilder =
            services.AddMvc(config =>
            {
                config.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                config.Filters.Add(new AuthorizeFilter());
            }) // Use session based TempData instead of cookie based TempData            
            .AddSessionStateTempDataProvider();         

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {   // dotnet add package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
            mvcBuilder.AddRazorRuntimeCompilation();
        }

        mvcBuilder.AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

        // dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
        mvcBuilder.AddNewtonsoftJson(options =>
         {
             // Use the default property (Pascal) casing
             options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
             options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
         });

        services._RegisterServices();

        return services;
    }

    public static IApplicationBuilder _InitPipeline(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        return app;
    }
}