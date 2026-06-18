using PointOfSale.Application.DTOs.Request.Users;
using PointOfSale.Application.DTOs.Response.Users;

namespace PointOfSale.Application.Services.Users.Create
{
    public interface IUserCreateService
    {
        Task<UserCreateResponse> CreateUserAsync(UserCreateRequest request);
    }
}
