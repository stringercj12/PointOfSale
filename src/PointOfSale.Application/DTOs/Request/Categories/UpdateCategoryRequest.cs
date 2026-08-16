namespace PointOfSale.Application.DTOs.Request.Categories
{
    public record UpdateCategoryRequest(string Name, string Description, bool Active);
}
