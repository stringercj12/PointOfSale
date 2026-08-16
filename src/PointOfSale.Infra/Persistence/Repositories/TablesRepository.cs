using Microsoft.EntityFrameworkCore;
using PointOfSale.Domain.Entities;
using PointOfSale.Domain.Repositories;
using PointOfSale.Infra.DataAccess;

namespace PointOfSale.Infra.Persistence.Repositories
{
    public class TablesRepository : ITablesRepository
    {

        private readonly PointOfSaleDbContext _context;

        public TablesRepository(PointOfSaleDbContext context)
        {
            _context = context;
        }

        public void Add(Table table)
        {
            _context.Tables.Add(table);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Products.AnyAsync(t => t.Name == name.Trim());
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            return await _context.Tables.AsNoTracking().ToListAsync();
        }

        public async Task<Table?> GetByIdAsync(Guid tableId)
        {
            return await _context.Tables.Where(t => t.Id == tableId).FirstOrDefaultAsync();
        }

        public void Remove(Table table)
        {
            _context.Tables.Remove(table);
        }

        public void Update(Table table)
        {
            _context.Tables.Update(table);
        }
    }
}
