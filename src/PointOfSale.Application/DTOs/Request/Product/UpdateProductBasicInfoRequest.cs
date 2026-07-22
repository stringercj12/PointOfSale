namespace PointOfSale.Application.DTOs.Request.Product;

public record UpdateProductBasicInfoRequest(
    string Name,
    string Description,
    decimal Price
);