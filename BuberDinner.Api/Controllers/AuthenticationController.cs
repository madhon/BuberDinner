namespace BuberDinner.Api.Controllers;

using BuberDinner.Api.Common.Mapping;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Domain.Common.Errors;
using Mediator;
using Microsoft.AspNetCore.Authorization;

[Route("auth")]
[AllowAnonymous]
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
        var command = AuthenticationMapper.ToRegisterCommand(request);
        var authResult = await mediator.Send(command);

        return authResult.Match(
            authResult => Ok(AuthenticationMapper.ToAuthenticationResponse(authResult)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = AuthenticationMapper.ToLoginQuery(request);

        var authResult = await mediator.Send(query);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(AuthenticationMapper.ToAuthenticationResponse(authResult)),
            errors => Problem(errors)
        );
    }
}