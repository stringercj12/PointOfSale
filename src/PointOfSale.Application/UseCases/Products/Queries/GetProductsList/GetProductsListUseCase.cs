using PointOfSale.Application.DTOs.Response.Attachment;
using PointOfSale.Application.DTOs.Response.Product;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Products.Queries.GetProductsList;

public class GetProductsListUseCase : IGetProductsListUseCase
{
    private readonly IProductRepository _productRepository;

    public GetProductsListUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyCollection<ProductResponse>> ExecuteAsync(GetProductsListRequest request)
    {
        var products = await _productRepository.GetFilteredAsync(request.OnlyActive, request.SearchTerm);

        return products.Select(product => new ProductResponse(
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
        )).ToList().AsReadOnly();
    }
}