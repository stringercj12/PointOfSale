namespace PointOfSale.Domain.Entities;

public class ProductCategory
{
    public ProductCategory(Guid productId, Guid categoryId)
    {
        ProductId = productId;
        CategoryId = categoryId;
    }

    public Guid ProductId { get; private set; }
    public Guid CategoryId { get; private set; }
}