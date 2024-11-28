namespace BuberDinner.Api;

using BuberDinner.Api.Common;
using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Common.Mapping;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;


public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddHttpLogging(o => o = new HttpLoggingOptions());

        services.AddOpenApi();
        
        services.AddControllers()
            .AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
            });
        
        services.AddMediator(opts => opts.ServiceLifetime = ServiceLifetime.Scoped);
        services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }
}