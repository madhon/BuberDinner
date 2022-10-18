namespace BuberDinner.Application.Authentication.Commands.Register;

using BuberDinner.Application.Authentication.Common;
using MediatR;
using ErrorOr;

public record RegisterCommand(
    string firstName, 
    string lastName, 
    string email, 
    string password) : IRequest<ErrorOr<AuthenticationResult>>;