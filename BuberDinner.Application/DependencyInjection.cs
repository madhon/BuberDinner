namespace BuberDinner.Application;


using BuberDinner.Application.Common.Behaviors;
using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        return services;
    }


}