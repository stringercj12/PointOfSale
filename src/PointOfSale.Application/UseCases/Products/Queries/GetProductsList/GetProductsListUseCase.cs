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

    public async Task<IEnumerable<ProductListResponse>> ExecuteAsync(GetProductsListRequest request)
    {
        var products = await _productRepository.GetFilteredAsync(request.OnlyActive, request.SearchTerm);

        return products.Select(product => new ProductListResponse(
            product.Id,
            product.Name,
            product.SKU,
            product.Price,
            product.ProductCategories
                .Select(c => c.CategoryId)
                .ToList(),
            product.Active,
            product.ProductAttachments
                .FirstOrDefault(pa => pa.IsPrimary)?
                .Attachment.Path
        )).ToList();
    }
}