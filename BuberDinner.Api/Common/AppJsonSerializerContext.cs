namespace BuberDinner.Api.Common;

using System.Text.Json;
using System.Text.Json.Serialization;
using BuberDinner.Contracts.Authentication;

[JsonSourceGenerationOptions(defaults: JsonSerializerDefaults.Web, 
    GenerationMode = JsonSourceGenerationMode.Default,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
    PropertyNamingPolicy =  JsonKnownNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true,
    AllowTrailingCommas = true)]
[JsonSerializable(typeof(LoginRequest))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
    
}