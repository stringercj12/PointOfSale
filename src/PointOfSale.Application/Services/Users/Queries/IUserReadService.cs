using PointOfSale.Application.DTOs.Response.Users;

namespace PointOfSale.Application.Services.Users.Get
{
    public interface IUserReadService
    {
        Task<UserResponse> FindByIdAsync(int userId);
        Task<IEnumerable<UserResponse>> GetAllActiveAsync(bool active);
    }
}
