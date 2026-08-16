
using PointOfSale.Application.DTOs.Response.Product;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Products.Queries.GetProductsByCategory
{
    public class GetProductsByCategoryUseCase : IGetProductsByCategoryUseCase
    {
        private readonly IProductRepository _productRepository;
        public GetProductsByCategoryUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductListResponse>> ExecuteAsync(Guid categoryId)
        {
            var products = await _productRepository.GetFilteredByCategoryIdAsync(categoryId);

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
}
