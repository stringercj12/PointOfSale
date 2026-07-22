namespace PointOfSale.Application.DTOs.Response.Product;

public record CategoryResponse(
    Guid CategoryId,
    string Name,
    string Description,
    bool Active,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);