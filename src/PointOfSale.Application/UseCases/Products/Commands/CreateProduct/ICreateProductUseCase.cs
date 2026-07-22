using PointOfSale.Application.DTOs.Request.Product;
using PointOfSale.Application.DTOs.Response.Product;

namespace PointOfSale.Application.UseCases.Products.Commands.CreateProduct;

public interface ICreateProductUseCase
{
    Task<ProductResponse> ExecuteAsync(CreateProductRequest request);
}