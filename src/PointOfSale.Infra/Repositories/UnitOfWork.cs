using PointOfSale.Infra.DataAccess;
using PointOfSale.Infra.Repositories.Interfaces;

namespace PointOfSale.Infra.Repositories
{
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
}
