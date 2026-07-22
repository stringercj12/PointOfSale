using Microsoft.AspNetCore.Http;

namespace PointOfSale.Application.DTOs.Request.Product;

public record UpdateProductPhotosRequest(
    Guid ProductId,
    List<IFormFile> ProductPhotos
);