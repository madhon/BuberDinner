namespace BuberDinner.Api.Common.Mapping;

using System.Reflection;
using BuberDinner.Application.Authentication.Queries.Login;
using Mapster;
using MapsterMapper;

public static class DependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        
        return services;
    }
}