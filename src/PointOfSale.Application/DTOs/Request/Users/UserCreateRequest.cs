using Microsoft.AspNetCore.Http;
using PointOfSale.Domain.Enums;

namespace PointOfSale.Application.DTOs.Request.Users;

public record UserCreateRequest(
    string Name,
    string Username,
    string Email,
    string Password,
    Role Role,
    bool Active,
    IFormFile? ProfileImage
);