namespace PointOfSale.Infra.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(); 
    }
}
