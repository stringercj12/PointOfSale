using PointOfSale.Domain.Exceptions;

namespace PointOfSale.Domain.Entities;

public class Category
{
    private Category()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool Active { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public IReadOnlyCollection<ProductCategory> Products => _products.AsReadOnly();
    private readonly List<ProductCategory> _products = [];

    public static Category CreateCategory(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Informe um nome para a categoria.");

        return new Category
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Active = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateCategory(string name, string description, bool active)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Informe um nome para a categoria.");

        Name = name;
        Description = description;
        Active = active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ToggleActiveStatus()
    {
        Active = !Active;
        UpdatedAt = DateTime.UtcNow;
    }
}