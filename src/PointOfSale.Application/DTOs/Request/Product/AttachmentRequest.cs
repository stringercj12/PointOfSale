using Microsoft.AspNetCore.Http;

namespace PointOfSale.Application.DTOs.Request.Product;

public record AttachmentRequest(
    List<IFormFile> files
);