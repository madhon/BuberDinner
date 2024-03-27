namespace BuberDinner.Api.Common.Mapping;

using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Contracts.Authentication;
using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class RegisterCommandMapper
{
    
    public static partial RegisterCommand MapToRegisterCommand(RegisterRequest source);
}