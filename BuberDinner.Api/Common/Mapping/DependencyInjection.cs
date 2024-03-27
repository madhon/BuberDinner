namespace BuberDinner.Api.Common.Mapping;

using System.Reflection;
using FastExpressionCompiler;
using Mapster;
using MapsterMapper;

public static class DependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Compiler = exp => exp.CompileFast();
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        
        return services;
    }
}