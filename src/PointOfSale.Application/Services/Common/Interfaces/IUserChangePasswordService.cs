namespace PointOfSale.Application.Services.Common.Interfaces
{
    public interface IUserChangePasswordService
    {
        Task ExecuteAsync(int userId, string password);
    }
}
