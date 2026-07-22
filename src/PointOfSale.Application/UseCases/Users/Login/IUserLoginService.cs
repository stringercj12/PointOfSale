using PointOfSale.Application.DTOs.Request.Login;
using PointOfSale.Application.DTOs.Response.Login;

namespace PointOfSale.Application.UseCases.Users.Login;

public interface IUserLoginService
{
    Task<LoginResponse> ExecuteAsync(LoginRequest request);
}