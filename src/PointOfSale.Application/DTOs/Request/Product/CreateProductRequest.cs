namespace PointOfSale.Application.DTOs.Request.Product;

public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string SKU,
    List<Guid> CategoryIds
);