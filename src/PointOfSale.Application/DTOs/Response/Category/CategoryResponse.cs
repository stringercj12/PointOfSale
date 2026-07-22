namespace PointOfSale.Application.DTOs.Response.Category;

public record CategoryResponse(
    Guid Id,
    string name,
    string description,
    bool active
);