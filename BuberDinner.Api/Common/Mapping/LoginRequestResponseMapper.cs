namespace BuberDinner.Api.Common.Mapping;

using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class LoginRequestQueryMapper
{

    public static partial LoginQuery MapRequestToQuery(LoginRequest request);


}