namespace PointOfSale.Application.DTOs.Response.Attachment;

public record AttachmentResponse(
    Guid AttachmentId,
    string Url,
    string Extension,
    string ContentType,
    string Name,
    string OriginalName,
    bool IsPrimary,
    int DisplayOrder
);