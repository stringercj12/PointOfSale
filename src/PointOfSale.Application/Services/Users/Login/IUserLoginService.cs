using PointOfSale.Application.DTOs.Request.Login;
using PointOfSale.Application.DTOs.Response.Login;

namespace PointOfSale.Application.Services.Users.Login
{
    public interface IUserLoginService
    {
        Task<LoginResponse> ExecuteAsync(LoginRequest request);
    }
}
