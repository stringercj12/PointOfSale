using PointOfSale.Application.DTOs.Request.Login;

namespace PointOfSale.Application.UseCases.Common.Interfaces;

public interface IUserChangePasswordService
{
    Task ExecuteAsync(ResetPasswordRequest request);
}