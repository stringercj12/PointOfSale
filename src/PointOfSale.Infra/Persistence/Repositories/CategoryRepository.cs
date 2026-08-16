using Microsoft.EntityFrameworkCore;
using PointOfSale.Domain.Entities;
using PointOfSale.Domain.Repositories;
using PointOfSale.Infra.DataAccess;

namespace PointOfSale.Infra.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly PointOfSaleDbContext _context;

    public CategoryRepository(PointOfSaleDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAll()
    {
        return await _context.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<Category?> GetById(Guid id)
    {
        return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category?> GetForUpdateAsync(Guid id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> ExistsByName(string name)
    {
        return await _context.Categories
            .AnyAsync(c => c.Name == name.Trim());
    }

    public async Task<IReadOnlyCollection<Category>> GetByIds(IEnumerable<Guid> ids)
    {
        if (ids == null || !ids.Any()) return Array.Empty<Category>();

        var categoryIds = ids.ToList();

        return await _context.Categories
            .AsNoTracking()
            .Where(c => categoryIds.Contains(c.Id))
            .ToListAsync();
    }

    public async Task Add(Category category)
    {
        await _context.Categories.AddAsync(category);
    }

    public void Update(Category category)
    {
        _context.Categories.Update(category);
    }

    public void Delete(Category category)
    {
        _context.Categories.Remove(category);
    }
}