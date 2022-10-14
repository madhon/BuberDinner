namespace BuberDinner.Application.Common.Interfaces.Authentication;

using BuberDinner.Domain.Entites;

public interface IJwtTokenGenerator 
{
    string GenerateToken(User user);
}