namespace BuberDinner.Infrastructure.Authentication;

using System.Security.Claims;
using System.Text;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Domain.Entites;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly TimeProvider dateTimeProvider;
    private readonly JwtSettings jwtSettings;

    public JwtTokenGenerator(TimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings)
    {
        this.dateTimeProvider = dateTimeProvider;
        this.jwtSettings = jwtSettings.Value;
    }
    
    public string GenerateToken(User user)
    {
        var now = dateTimeProvider.GetUtcNow().UtcDateTime;
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim("sub",  user.Id.ToString()),
            new Claim("given_name", user.FirstName),
            new Claim("family_name", user.LastName),
            new Claim("jti", Guid.NewGuid().ToString()),
        };

        var securityToken = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = jwtSettings.Issuer,
            Audience = jwtSettings.Audience,
            NotBefore = now,
            Expires = now.AddMinutes(jwtSettings.ExpiryMinutes),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JsonWebTokenHandler();

        return tokenHandler.CreateToken(securityToken);
    }
}