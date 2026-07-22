using PointOfSale.Application.DTOs.Response.Product;

namespace PointOfSale.Application.UseCases.Products.Queries.GetProductById;

public interface IGetProductByIdUseCase
{
    Task<ProductResponse> ExecuteAsync(Guid productId);
}