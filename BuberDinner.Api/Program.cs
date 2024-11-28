using BuberDinner.Api;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateSlimBuilder(args);
{
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ConfigureEndpointDefaults(listenOptions =>
        {
            listenOptions.UseHttps();
        });
    });
    
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
	app.UseHttpLogging();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapOpenApi();
    app.MapScalarApiReference(opts => opts.DefaultFonts = false);
    app.MapControllers();
    app.Run();
}
