namespace BuberDinner.Application;

using BuberDinner.Application.Common.Behaviors;
using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using ServiceScan.SourceGenerator;

public static partial class DependencyInjection
{
    [GenerateServiceRegistrations(AssignableTo = typeof(IValidator<>), Lifetime = ServiceLifetime.Singleton)]
    public static partial IServiceCollection AddValidators(this IServiceCollection services);
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidators();
        
        return services;
    }
}