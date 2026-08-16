using PointOfSale.Domain.Enums;

namespace PointOfSale.Application.DTOs.Request.Users;

public record UserUpdateRequest(
    string Name,
    string Email,
    Role Role,
    bool Active
);