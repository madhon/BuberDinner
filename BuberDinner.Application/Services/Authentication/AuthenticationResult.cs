namespace BuberDinner.Application.Services.Authentication;

using BuberDinner.Domain.Entites;

public record AuthenticationResult(
    User User,
    string Token);