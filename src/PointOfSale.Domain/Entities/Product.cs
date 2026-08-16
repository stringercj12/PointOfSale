using PointOfSale.Domain.Exceptions;

namespace PointOfSale.Domain.Entities;

public class Product
{


    protected Product()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string SKU { get; private set; } = string.Empty;
    public bool Active { get; private set; }
    public IReadOnlyCollection<ProductCategory> ProductCategories => _productCategories.AsReadOnly();
    private readonly List<ProductCategory> _productCategories = [];
    public IReadOnlyCollection<ProductAttachment> ProductAttachments => _productAttachments.AsReadOnly();
    private readonly List<ProductAttachment> _productAttachments = [];

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public static Product Create(string name, string description, decimal price, string sku)
    {
        ValidateProductState(name, price);

        if (string.IsNullOrWhiteSpace(sku))
            throw new DomainException("O SKU do produto é obrigatório.");

        return new Product
        {
            Id = Guid.NewGuid(),
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

    public void UpdateCategories(IEnumerable<Category> categories)
    {
        EnsureIsActive();

        if (categories is null)
            throw new DomainException("Informe pelo menos uma categoria.");

        _productCategories.Clear();

        foreach (var category in categories) _productCategories.Add(new ProductCategory(this, category));

        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateAttachments(IEnumerable<(Attachment Attachment, bool IsPrimary, int DisplayOrder)> attachments)
    {
        EnsureIsActive();

        if (attachments.Count(a => a.IsPrimary) > 1)
            throw new DomainException("O produto só pode possuir apenas uma foto principal.");

        if (attachments is null)
            throw new DomainException("Anexo inválido.");

        var attachmentList = attachments.ToList();

        if (attachmentList.Count > 5)
            throw new DomainException("Um produto não pode ter mais que 5 fotos.");

        _productAttachments.Clear();



        foreach (var item in attachmentList)
        {

            if (item.IsPrimary)
            {
                foreach (var attachment in _productAttachments)
                    attachment.SetPrimary(false);
            }


            _productAttachments.Add(new ProductAttachment(this, item.Attachment, item.IsPrimary, item.DisplayOrder));
        }

        UpdatedAt = DateTime.UtcNow;
    }

    public void AddAttachment((Attachment Attachment, bool IsPrimary, int DisplayOrder) item)
    {
        EnsureIsActive();


        if (_productAttachments.Count >= 5)
            throw new DomainException("Um produto não pode possuir mais de 5 fotos.");

        if (item.IsPrimary)
        {
            foreach (var attachment in _productAttachments)
                attachment.SetPrimary(false);
        }


        _productAttachments.Add(new ProductAttachment(this, item.Attachment, item.IsPrimary, item.DisplayOrder));

        UpdatedAt = DateTime.UtcNow;
    }

    public void SetPrimaryAttachment(Guid attachmentId)
    {
        EnsureIsActive();

        var attachment = _productAttachments.FirstOrDefault(a => a.AttachmentId == attachmentId);

        if (attachment is null)
            throw new DomainException("A foto informada não pertence ao produto.");

        foreach (var item in _productAttachments)
            item.SetPrimary(false);

        attachment.SetPrimary(true);

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