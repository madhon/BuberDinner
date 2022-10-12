using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResponse = authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        var response = new AuthenticationResponse(
            authResponse.Id, 
            authResponse.FirstName,
            authResponse.LastName, 
            authResponse.Email,
            authResponse.Token
        );

        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResponse = authenticationService.Login(request.Email, request.Password);

        var response = new AuthenticationResponse(
            authResponse.Id, 
            authResponse.FirstName,
            authResponse.LastName, 
            authResponse.Email,
            authResponse.Token
        );

        return Ok(response);
    }
}