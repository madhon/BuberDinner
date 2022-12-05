namespace BuberDinner.Application.Authentication.Queries.Login;

using BuberDinner.Application.Authentication.Common;
using ErrorOr;
using Mediator;

public record LoginQuery(
    string Email, 
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;