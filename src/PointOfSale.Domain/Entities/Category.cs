namespace PointOfSale.Domain.Entities;

public class Category
{
    private Category()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool Active { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public List<Product> Products { get; private set; } = new();

    public static Category Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Informe um nome para a categoria.");

        return new Category
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Active = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void ToggleActiveStatus()
    {
        Active = !Active;
        UpdatedAt = DateTime.UtcNow;
    }
}