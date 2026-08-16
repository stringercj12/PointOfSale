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

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.ProductAttachments)
                .ThenInclude(pa => pa.Attachment)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Product?> GetBySkuAsync(string sku)
    {
        return await _context.Products
           .AsNoTracking()
           .Include(p => p.ProductAttachments)
               .ThenInclude(pa => pa.Attachment)
           .FirstOrDefaultAsync(x => x.SKU == sku);
    }

    public async Task<Product?> GetForUpdateAsync(Guid id)
    {
        return await _context.Products
            .Include(p => p.ProductAttachments)
                .ThenInclude(pa => pa.Attachment)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Product>> GetFilteredAsync(bool? onlyActive, string? searchTerm)
    {
        var query = _context.Products
            .AsNoTracking()
            .Include(p => p.ProductCategories)
            .Include(p => p.ProductAttachments)
            .ThenInclude(pa => pa.Attachment)
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
    public async Task<IEnumerable<Product>> GetFilteredByCategoryIdAsync(Guid categoryId)
    {
        var query = _context.Products
            .AsNoTracking()
            .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId))
            .Include(p => p.ProductCategories)
            .Include(p => p.ProductAttachments)
                .ThenInclude(pa => pa.Attachment)
            .AsQueryable();


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