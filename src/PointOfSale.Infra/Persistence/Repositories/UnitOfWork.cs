using PointOfSale.Domain.Repositories;
using PointOfSale.Infra.DataAccess;

namespace PointOfSale.Infra.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PointOfSaleDbContext _context;

    public UnitOfWork(PointOfSaleDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}