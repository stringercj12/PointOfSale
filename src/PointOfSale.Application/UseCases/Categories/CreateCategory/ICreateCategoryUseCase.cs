using PointOfSale.Application.DTOs.Request.Categories;

namespace PointOfSale.Application.UseCases.Categories.CreateCategory;

public interface ICreateCategoryUseCase
{
    Task ExecuteAsync(CreateCategoryRequest request);
}