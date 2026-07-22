namespace PointOfSale.Application.DTOs.Response.Attachment;

public record AttachmentResponse(
    Guid AttachmentId,
    string Url
);