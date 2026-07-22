using PointOfSale.Application.DTOs.Request.Users;
using PointOfSale.Application.DTOs.Response.Users;

namespace PointOfSale.Application.UseCases.Users.Create;

public interface IUserCreateService
{
    Task<UserCreateResponse> ExecuteAsync(UserCreateRequest request);
}