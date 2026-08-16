using PointOfSale.Domain.Entities;

namespace PointOfSale.Domain.Repositories
{
    public interface ITablesRepository
    {
        Task<Table?> GetByIdAsync(Guid tableId);
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<bool> ExistsByNameAsync(string name);
        void Add(Table table);
        void Update(Table table);
        void Remove(Table table);
    }
}
