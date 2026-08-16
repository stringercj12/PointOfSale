using PointOfSale.Domain.Entities;

namespace PointOfSale.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid productId);
    Task<Product?> GetBySkuAsync(string sku);
    Task<Product?> GetForUpdateAsync(Guid productId);

    Task<IEnumerable<Product>> GetFilteredAsync(bool? onlyActive, string? searchTerm);
    Task<IEnumerable<Product>> GetFilteredByCategoryIdAsync(Guid categoryId);
    Task<bool> ExistsBySkuAsync(string sku);
    void Add(Product product);
    void Update(Product product);
    void Remove(Product product);
}