using PointOfSale.Application.DTOs.Request.Product;

namespace PointOfSale.Application.UseCases.Products.Commands.UpdateProductCategories;

public interface IUpdateProductCategoriesUseCase
{
    Task ExecuteAsync(UpdateProductCategoriesRequest request);
}