using PointOfSale.Application.DTOs.Response.Attachment;

namespace PointOfSale.Application.DTOs.Response.Product;

public record ProductResponse(
    Guid ProductId,
    string Name,
    string Description,
    decimal Price,
    string SKU,
    bool Active,
    IReadOnlyCollection<Guid> Categories,
    IReadOnlyCollection<AttachmentResponse> Attachments,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);