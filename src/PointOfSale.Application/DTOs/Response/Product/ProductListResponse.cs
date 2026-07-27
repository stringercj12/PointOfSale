namespace PointOfSale.Application.DTOs.Response.Product
{
    public record ProductListResponse(
         Guid ProductId,
         string Name,
         string SKU,
         decimal Price,
         IEnumerable<Guid> Categories,
         bool Active,
         string? ThumbnailUrl
    );
}
