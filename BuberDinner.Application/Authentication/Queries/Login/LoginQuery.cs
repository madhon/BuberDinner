namespace BuberDinner.Application.Authentication.Queries.Login;

using BuberDinner.Application.Authentication.Common;
using MediatR;
using ErrorOr;

public record LoginQuery(
    string Email, 
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;