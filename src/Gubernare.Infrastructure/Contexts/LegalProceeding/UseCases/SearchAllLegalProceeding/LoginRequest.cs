using System.Text.Json.Serialization;

namespace Gubernare.Infrastructure.Contexts.LegalProceeding.UseCases.SearchAllLegalProceeding;

public class LoginRequest
{
    [JsonPropertyName("login")]
    public required string Login { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }
}