using Microsoft.AspNetCore.Http;

namespace PointOfSale.Application.DTOs.Request.Product;

public record UpdateProductPhotosRequest(
    Guid ProductId,
    List<ProductPhotoListRequest> ProductPhotos
);


public record ProductPhotoListRequest(IFormFile Photo, bool IsPrimary, int DisplayOrder);