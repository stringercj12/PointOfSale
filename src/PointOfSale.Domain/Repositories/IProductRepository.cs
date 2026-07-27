using PointOfSale.Domain.Entities;

namespace PointOfSale.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid productId);
    Task<Product?> GetForUpdateAsync(Guid productId);

    Task<IEnumerable<Product>> GetFilteredAsync(bool? onlyActive, string? searchTerm);
    Task<bool> ExistsBySkuAsync(string sku);
    void Add(Product product);
    void Update(Product product);
    void Remove(Product product);
}