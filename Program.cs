using MvcZero.AppLib.Configuration;

try
{
	var builder = WebApplication.CreateBuilder(args)._ConfigureServices();

	var app = builder.Build()._Configure().Result;

	app.Run();
}
catch (Exception ex)
{
	Host.CreateDefaultBuilder(args)
	.ConfigureServices(services => { services.AddMvc(); })
	.ConfigureWebHostDefaults(webBuilder =>
	{
		webBuilder.Configure((ctx, app) =>
		{
			app.Run(async (context) =>
			{
				await context.Response.WriteAsync($"Error in application: {ex.Message}");
			});
		});
	}).Build().Run();
}