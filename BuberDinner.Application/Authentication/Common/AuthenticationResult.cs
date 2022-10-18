namespace BuberDinner.Application.Authentication.Common;

using BuberDinner.Domain.Entites;

public record AuthenticationResult(User User, string Token);