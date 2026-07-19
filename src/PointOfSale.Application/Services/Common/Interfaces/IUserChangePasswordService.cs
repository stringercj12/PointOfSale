using PointOfSale.Application.DTOs.Request.Login;

namespace PointOfSale.Application.Services.Common.Interfaces
{
    public interface IUserChangePasswordService
    {
        Task ExecuteAsync(ResetPasswordRequest request);
    }
}
