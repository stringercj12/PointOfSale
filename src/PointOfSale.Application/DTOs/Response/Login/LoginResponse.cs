using PointOfSale.Domain.Enums;
using System.Text.Json.Serialization;

namespace PointOfSale.Application.DTOs.Response.Login
{
    public record LoginResponse(
        string Token,
        string TokenType,
        long ExpiresIn,
        UserSessionInfo User
    );

    public record UserSessionInfo(
        int Id,
        string Name,
        string Email,

        [property: JsonConverter(typeof(JsonStringEnumConverter))]
        [property: JsonPropertyName("role")] RoleEnum Role,
        string? ProfileImage
    );
}
