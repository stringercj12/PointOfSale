using PointOfSale.Application.DTOs.Response.Attachment;

namespace PointOfSale.Application.DTOs.Response.Product;

public record ProductResponse(
    Guid ProductId,
    string Name,
    string Description,
    decimal Price,
    string SKU,
    bool Active,
    IEnumerable<CategoryResponse> Categories,
    IEnumerable<AttachmentResponse> Attachments,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);