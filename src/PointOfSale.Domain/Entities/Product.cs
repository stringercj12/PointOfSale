using PointOfSale.Domain.Exceptions;

namespace PointOfSale.Domain.Entities;

public class Product
{
    private readonly List<Attachment> _attachments = [];

    private readonly List<ProductCategory> _productCategories = [];

    protected Product()
    {
    }

    public int Id { get; private set; }
    public Guid ProductId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string SKU { get; private set; } = string.Empty;
    public bool Active { get; private set; }
    public IReadOnlyCollection<ProductCategory> ProductCategories => _productCategories.AsReadOnly();
    public IReadOnlyCollection<Attachment> Attachments => _attachments.AsReadOnly();

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public static Product Create(string name, string description, decimal price, string sku)
    {
        ValidateProductState(name, price);

        if (string.IsNullOrWhiteSpace(sku))
            throw new DomainException("O SKU do produto é obrigatório.");

        return new Product
        {
            ProductId = Guid.NewGuid(),
            Name = name.Trim(),
            Description = description.Trim(),
            Price = price,
            SKU = sku.Trim().ToUpperInvariant(),
            Active = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateProduct(string name, string description, decimal price)
    {
        EnsureIsActive();
        ValidateProductState(name, price);

        Name = name.Trim();
        Description = description.Trim();
        Price = price;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateCategories(IEnumerable<Guid> categoriesIds)
    {
        EnsureIsActive();

        if (categoriesIds is null)
            throw new DomainException("Informe pelo menos uma categoria.");

        _productCategories.Clear();
        foreach (var categoryId in categoriesIds) _productCategories.Add(new ProductCategory(ProductId, categoryId));

        UpdatedAt = DateTime.UtcNow;
    }

    public void AddAttachment(Attachment attachment)
    {
        EnsureIsActive();

        if (attachment is null)
            throw new DomainException("Anexo inválido.");

        if (_attachments.Count >= 5)
            throw new DomainException("Um produto não pode ter mais que 5 fotos.");

        _attachments.Add(attachment);
        UpdatedAt = DateTime.UtcNow;
    }

    public void ToggleActiveStatus()
    {
        Active = !Active;
        UpdatedAt = DateTime.UtcNow;
    }

    private void EnsureIsActive()
    {
        if (!Active)
            throw new DomainException("Não é possível alterar um produto inativo. Ative-o primeiro.");
    }

    private static void ValidateProductState(string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("O nome do produto é obrigatório.");

        if (price <= 0)
            throw new DomainException("O preço do produto deve ser maior que zero.");
    }
}