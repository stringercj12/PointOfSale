using PointOfSale.Application.DTOs.Request.Users;

namespace PointOfSale.Application.Services.Users.Update;

public interface IUserUpdateService
{
    Task ExecuteAsync(UserUpdateRequest request, Guid userId);
}