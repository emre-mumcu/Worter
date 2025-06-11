namespace Wörter.AppLib.Configuration.Ext;

public static class Authorization
{
    public static IServiceCollection _AddAuthorization(this IServiceCollection services)
    {
        return services;
    }

    public static IApplicationBuilder _UseAuthorization(this WebApplication app)
    {

        app.UseAuthorization();
        return app;
    }
}
