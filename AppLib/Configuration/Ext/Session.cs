using Newtonsoft.Json;

namespace Wörter.AppLib.Configuration.Ext;

public static class Session
{
    public static IServiceCollection _AddSession(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();

        services.AddSession(options =>
        {
            options.Cookie.Name = AppConstants.Session_Cookie_Name;
            options.IdleTimeout =  TimeSpan.FromMinutes(AppConstants.Session_IdleTimeout);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        return services;
    }

    public static IApplicationBuilder _UseSession(this WebApplication app)
    {
        app.UseSession();

        return app;
    }
}

public static class SessionExtensions
{
    private static JsonSerializerSettings jsonSerializerSettings
    {
        get
        {
            return new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                Culture = new System.Globalization.CultureInfo("tr-TR")
            };
        }
    }

    public static void SetKey<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value, Formatting.None, jsonSerializerSettings));
    }

    public static T? GetKey<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonConvert.DeserializeObject<T>(value);
    }

    public static void RemoveKey(this ISession session, string key)
    {
        session.Remove(key);
    }
}
