namespace BuberDinner.Infrastructure.Authentication;

using Microsoft.Extensions.Options;

[OptionsValidator]
internal sealed partial class JwtSettingsValidator : IValidateOptions<JwtSettings>;