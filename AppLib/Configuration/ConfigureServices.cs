using Wörter.AppLib.Configuration.Ext;

namespace MvcZero.AppLib.Configuration;

public static class ConfigureServices
{
    public static WebApplicationBuilder _ConfigureServices(this WebApplicationBuilder web_builder)
    {
        web_builder.Services._InitServices();

        web_builder.Services._AddSession();

        web_builder.Services._AddAuthentication();

		web_builder.Services.AddDbContext<Wörter.AppData.AppDbContext>();

		web_builder.Services.AddAutoMapper(typeof(Program));

		return web_builder;
    }
}