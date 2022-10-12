namespace BuberDinner.Infrastructure.Authentication;

using System.Security.Claims;
using System.Text;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly JwtSettings jwtSettings;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings)
    {
        this.dateTimeProvider = dateTimeProvider;
        this.jwtSettings = jwtSettings.Value;
    }
    
    public string GenerateToken(Guid id, string firstName, string lastName)
    {
        var now = dateTimeProvider.UtcNow;
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim("sub", id.ToString()),
            new Claim("given_name", firstName),
            new Claim("family_name", lastName),
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
   
        return new JsonWebTokenHandler().CreateToken(securityToken);
    }
}