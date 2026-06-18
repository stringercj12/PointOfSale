using Microsoft.AspNetCore.Http;
using PointOfSale.Domain.Enums;

namespace PointOfSale.Application.DTOs.Request.Users
{
    public record UserCreateRequest(
        string Name,
        string Username,
        string Email,
        string Password,
        RoleEnum Role,
        bool Active,
        IFormFile? ProfileImage
    );
}
