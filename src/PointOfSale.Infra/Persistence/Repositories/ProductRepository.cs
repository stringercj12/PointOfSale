using Microsoft.EntityFrameworkCore;
using PointOfSale.Domain.Entities;
using PointOfSale.Domain.Repositories;
using PointOfSale.Infra.DataAccess;

namespace PointOfSale.Infra.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly PointOfSaleDbContext _context;

    public ProductRepository(PointOfSaleDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(Guid productId)
    {
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.ProductCategories)
            .Include(p => p.Attachments)
            .FirstOrDefaultAsync(product => product.ProductId == productId);
    }

    public async Task<List<Product>> GetFilteredAsync(bool? onlyActive, string? searchTerm)
    {
        var query = _context.Products
            .AsNoTracking()
            .Include(p => p.ProductCategories)
            .Include(p => p.Attachments)
            .AsQueryable();

        if (onlyActive.HasValue)
            query = query.Where(p => p.Active == onlyActive.Value);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.Trim().ToLower();
            query = query.Where(p => p.Name.ToLower().Contains(term) || p.SKU.ToLower().Contains(term));
        }

        return await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
    }

    public async Task<bool> ExistsBySkuAsync(string sku)
    {
        return await _context.Products
            .AnyAsync(p => p.SKU == sku.Trim().ToUpperInvariant());
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }
}