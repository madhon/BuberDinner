namespace BuberDinner.Api.Controllers;

using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Domain.Common.Errors;
using MediatR;

[ApiController]
[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender mediator;
    
    public AuthenticationController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);

        var authResult = await mediator.Send(command);

        return authResult.Match( 
            authResult => Ok(MapResult(authResult)),
            errors => Problem(errors)
            );
    }
    
    [HttpPost("login")]
    public async Task<IActionResult>  Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        
        var authResult = await mediator.Send(query);
        
        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
        }
        
        return authResult.Match( 
            authResult => Ok(MapResult(authResult)),
            errors => Problem(errors)
        );
    }
    
    private static AuthenticationResponse MapResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
    }
}