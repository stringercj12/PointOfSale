using PointOfSale.Application.DTOs.Response.Product;

namespace PointOfSale.Application.UseCases.Products.Queries.GetProductById;

public interface IGetProductBySkuUseCase
{
    Task<ProductResponse> ExecuteAsync(string sku);
}