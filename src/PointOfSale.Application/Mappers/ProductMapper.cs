using PointOfSale.Application.DTOs.Response.Attachment;
using PointOfSale.Application.DTOs.Response.Product;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Application.Mappers;

public static class ProductMapper
{
    public static ProductResponse ToResponse(this Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        var attachments = product.ProductAttachments
            .Select(a => new AttachmentResponse(
                a.Attachment.Id,
                a.Attachment.Path,
                a.Attachment.Extension,
                a.Attachment.ContentType,
                a.Attachment.Name,
                a.Attachment.OriginalName,
                a.IsPrimary,
                a.DisplayOrder
                ))
            .ToList()
            .AsReadOnly();

        var categories = product.ProductCategories
            .Select(c => c.CategoryId)
            .ToList()
            .AsReadOnly();

        return new ProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.SKU,
            product.Active,
            categories,
            attachments,
            product.CreatedAt,
            product.UpdatedAt
        );
    }
}