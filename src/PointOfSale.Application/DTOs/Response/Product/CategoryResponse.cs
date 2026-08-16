namespace PointOfSale.Application.DTOs.Response.Product;

public record CategoryResponse(
    Guid Id,
    string Name,
    string Description,
    bool Active,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);