namespace PointOfSale.Domain.Entities;

public class ProductCategory
{
    private ProductCategory() { }

    public ProductCategory(Product product, Category attachment)
    {
        Product = product;
        ProductId = product.Id;

        Category = attachment;
        CategoryId = attachment.Id;
    }

    public Guid ProductId { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    public Product Product { get; private set; } = null!;


}