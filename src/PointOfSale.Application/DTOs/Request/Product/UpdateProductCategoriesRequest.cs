namespace PointOfSale.Application.DTOs.Request.Product;

public record UpdateProductCategoriesRequest(
    Guid ProductId,
    List<Guid> CategoryIds
);