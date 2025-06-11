﻿using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Wörter.AppLib.Configuration.Ext;

public static class Services
{
    public static IServiceCollection _RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        services.AddHttpClient();

        services.AddTransient<RazorTools>();
        
        return services;        
    }
}



//services.Add(new ServiceDescriptor(typeof(IDataService), typeof(DataService), ServiceLifetime.Transient));

// without interface
// services.AddTransient<FruitServices>();  

// 

// services.AddSingleton<IConnectionFactory>(s =>  new SqlConnectionFactory(connString)); // if constructor params are required
// _serviceCollection.AddSingleton<IService>(x =>   ActivatorUtilities.CreateInstance<Service>(x, ""););
//var myServiceFactory = ActivatorUtilities.CreateFactory(typeof(MyService), new object[] { typeof(IOtherService), });
//MyService myService = myServiceFactory(serviceProvider, myServiceOrParameterTypeToReplace);
//myService = ActivatorUtilities.CreateInstance<Service>(serviceProvider,          new OtherServiceB());
//_serviceCollection.AddSingleton<IOtherService, OtherService>();
//_serviceCollection.AddSingleton<IAnotherOne, AnotherOne>();
//_serviceCollection.AddSingleton<IService>(x => new Service(x.GetService<IOtherService>(), x.GetService<IAnotherOne>(), ""));
//services.AddSingleton<IOtherService, OtherService>();
//services.AddSingleton<IAnotherOne, AnotherOne>();
//services.AddSingleton<IService>(x =>
//    new Service(
//        services.BuildServiceProvider().GetService<IOtherService>(),
//        services.BuildServiceProvider().GetService<IAnotherOne>(),
//        ""));
//https://www.tutorialsteacher.com/core/dependency-injection-in-aspnet-core

//https://joonasw.net/view/aspnet-core-di-deep-dive
//Implementation factories
//services.AddTransient<IDataService, DataService>((ctx) =>
//{
//    IOtherService svc = ctx.GetService<IOtherService>();
//    //IOtherService svc = ctx.GetRequiredService<IOtherService>();
//    return new DataService(svc);
//});
