using PointOfSale.Application.DTOs.Response.Attachment;
using PointOfSale.Application.DTOs.Response.Product;
using PointOfSale.Application.Exceptions;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Products.Queries.GetProductById;

public class GetProductByIdUseCase : IGetProductByIdUseCase
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponse> ExecuteAsync(Guid productId)
    {
        var product = await _productRepository.GetByIdAsync(productId)
                      ?? throw new NotFoundException($"Produto com ID '{productId}' não foi encontrado.");

        return new ProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.SKU,
            product.Active,
            product.ProductCategories.Select(c => new CategoryResponse(
                c.CategoryId, 
                c.Category.Name,
                c.Category.Description, 
                c.Category.Active,
                c.Category.CreatedAt,
                c.Category.UpdatedAt)).ToList(),
            product.ProductAttachments.Select(a => new AttachmentResponse(
                a.Attachment.Id,
                a.Attachment.Path,
                a.Attachment.Extension,
                a.Attachment.ContentType,
                a.Attachment.Name,
                a.Attachment.OriginalName,
                a.IsPrimary,
                a.DisplayOrder
                )).ToList(),
            product.CreatedAt,
            product.UpdatedAt
        );
    }
}