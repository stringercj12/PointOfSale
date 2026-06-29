namespace PointOfSale.Application.Services.Users.Delete
{
    public interface IUserDeleteService
    {

        Task ExecuteAsync(int userId);
    }
}
