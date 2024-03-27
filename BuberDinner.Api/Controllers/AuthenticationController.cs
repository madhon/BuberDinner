namespace BuberDinner.Api.Controllers;

using BuberDinner.Api.Common.Mapping;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using BuberDinner.Domain.Common.Errors;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Authorization;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender mediator;
    private readonly IMapper mapper;
    
    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = RegisterCommandMapper.MapToRegisterCommand(request);
        var authResult = await mediator.Send(command);

        return authResult.Match( 
            authResult => Ok(mapper.Map<AuthenticationResult>(authResult)),
            errors => Problem(errors)
            );
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = LoginRequestQueryMapper.MapRequestToQuery(request);
        
        var authResult = await mediator.Send(query);
        
        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
        }
        
        return authResult.Match( 
            authResult => Ok(mapper.Map<AuthenticationResult>(authResult)),
            errors => Problem(errors)
        );
    }
}