using Microsoft.AspNetCore.Http;
using PointOfSale.Communication.Enums;

namespace PointOfSale.Communication.Request.User
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
