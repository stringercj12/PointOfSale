using PointOfSale.Application.DTOs.Response.Users;

namespace PointOfSale.Application.Services.Users.Get;

public interface IUserReadService
{
    Task<UserResponse> FindByIdAsync(Guid userId);
    Task<List<UserResponse>> GetAllActiveAsync(bool active);
}