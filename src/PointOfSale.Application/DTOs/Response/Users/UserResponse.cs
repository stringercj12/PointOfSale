using PointOfSale.Application.Extensions;
using PointOfSale.Domain.Entities;
using PointOfSale.Domain.Enums;

namespace PointOfSale.Application.DTOs.Response.Users;

public record UserResponse(
    int Id,
    string Name,
    string Username,
    string Email,
    RoleEnum Role,
    string RoleDescription,
    bool Active,
    string ProfileImage
)
{
    public static UserResponse From(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new UserResponse(
            user.Id,
            user.Name,
            user.Username,
            user.Email,
            user.Role,
            user.Role.GetDescription(),
            user.Active,
            user.ProfileImage ?? string.Empty
        );
    }
}