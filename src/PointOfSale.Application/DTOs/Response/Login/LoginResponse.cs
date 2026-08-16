using System.Text.Json.Serialization;
using PointOfSale.Domain.Enums;

namespace PointOfSale.Application.DTOs.Response.Login;

public record LoginResponse(
    string Token,
    string TokenType,
    long ExpiresIn,
    UserSessionInfo User
);

public record UserSessionInfo(
    Guid Id,
    string Name,
    string Email,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    [property: JsonPropertyName("role")]
    Role Role,
    string? ProfileImage
);