using Microsoft.AspNetCore.Http;
using PointOfSale.Domain.Enums;

namespace PointOfSale.Application.DTOs.Request.Users;

public record UserRequest(
    int Id,
    string Name,
    string Username,
    string Email,
    RoleEnum Role,
    bool Active,
    IFormFile? ProfileImage
);