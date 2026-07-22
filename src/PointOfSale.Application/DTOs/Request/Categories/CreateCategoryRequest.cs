namespace PointOfSale.Application.DTOs.Request.Categories;

public record CreateCategoryRequest(
    string Name,
    string Description
);