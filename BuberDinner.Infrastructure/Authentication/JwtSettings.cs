namespace BuberDinner.Infrastructure.Authentication;

using System.ComponentModel.DataAnnotations;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    [Required(ErrorMessage = "JWT Secret is required.")]
    public string Secret { get; set; } = string.Empty;
    public int ExpiryMinutes { get; set; }
    
    [Required(ErrorMessage = "JWT Issuer is required.")]
    public string Issuer { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "JWT Audience is required.")]
    public string Audience { get; set; } = string.Empty;
}
