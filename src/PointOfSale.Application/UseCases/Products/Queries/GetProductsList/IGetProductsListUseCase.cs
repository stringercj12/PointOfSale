using PointOfSale.Application.DTOs.Response.Product;

namespace PointOfSale.Application.UseCases.Products.Queries.GetProductsList;

public interface IGetProductsListUseCase
{
    Task<IEnumerable<ProductListResponse>> ExecuteAsync(GetProductsListRequest request);
}