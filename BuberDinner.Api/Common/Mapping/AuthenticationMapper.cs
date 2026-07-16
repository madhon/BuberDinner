namespace BuberDinner.Api.Common.Mapping;

using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class AuthenticationMapper
{
    public static partial RegisterCommand ToRegisterCommand(RegisterRequest request);

    public static partial LoginQuery ToLoginQuery(LoginRequest request);

    [MapProperty(nameof(AuthenticationResult.User.Id), nameof(AuthenticationResponse.Id))]
    [MapProperty(nameof(AuthenticationResult.User.FirstName), nameof(AuthenticationResponse.FirstName))]
    [MapProperty(nameof(AuthenticationResult.User.LastName), nameof(AuthenticationResponse.LastName))]
    [MapProperty(nameof(AuthenticationResult.User.Email), nameof(AuthenticationResponse.Email))]
    public static partial AuthenticationResponse ToAuthenticationResponse(AuthenticationResult result);
}