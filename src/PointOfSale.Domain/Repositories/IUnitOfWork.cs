namespace PointOfSale.Domain.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}