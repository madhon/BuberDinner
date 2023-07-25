namespace BuberDinner.Application.Common.Behaviors;

using ErrorOr;
using FluentValidation;
using Mediator;


public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        this.validator = validator;
    }
    
    public async ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
    {
        if (validator is null)
        {
            return await next(message, cancellationToken);
        }
        
        var validationResult = await validator.ValidateAsync(message, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next(message, cancellationToken);
        }

        var errors = validationResult.Errors.ConvertAll(vf => 
            Error.Validation(vf.PropertyName, vf.ErrorMessage));

        return (dynamic)errors;
    }
    
}