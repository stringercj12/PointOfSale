using PointOfSale.Application.DTOs.Response.Product;

namespace PointOfSale.Application.UseCases.Products.Queries.GetProductsByCategory
{
    public interface IGetProductsByCategoryUseCase
    {
        Task<IEnumerable<ProductListResponse>> ExecuteAsync(Guid categoryId);
    }
}
