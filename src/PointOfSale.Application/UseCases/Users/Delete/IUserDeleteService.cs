namespace PointOfSale.Application.UseCases.Users.Delete;

public interface IUserDeleteService
{
    Task ExecuteAsync(int userId);
}