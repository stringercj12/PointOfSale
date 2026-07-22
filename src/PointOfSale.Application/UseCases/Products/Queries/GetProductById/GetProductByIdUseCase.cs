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
        // 1. Busca no repositório
        var product = await _productRepository.GetByIdAsync(productId)
                      ?? throw new NotFoundException($"Produto com ID '{productId}' não foi encontrado.");

        // 2. Mapeia a Entidade para o DTO de Saída (Response)
        return new ProductResponse(
            product.ProductId,
            product.Name,
            product.Description,
            product.Price,
            product.SKU,
            product.Active,
            product.ProductCategories.Select(c => c.CategoryId).ToList(),
            product.Attachments.Select(a => new AttachmentResponse(a.Id, a.Path)).ToList(),
            product.CreatedAt,
            product.UpdatedAt
        );
    }
}