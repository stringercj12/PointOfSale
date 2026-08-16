using PointOfSale.Domain.Entities;

namespace PointOfSale.Domain.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAll();
    Task<Category?> GetById(Guid id);
    Task<Category?> GetForUpdateAsync(Guid id);
    Task<bool> ExistsByName(string name);
    Task<IReadOnlyCollection<Category>> GetByIds(IEnumerable<Guid> ids);
    Task Add(Category category);
    void Update(Category category);
    void Delete(Category category);
}