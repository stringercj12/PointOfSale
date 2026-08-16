using PointOfSale.Application.DTOs.Request.Categories;

namespace PointOfSale.Application.UseCases.Categories.Commands.UpdateCategory
{
    public interface IUpdateCategoryUseCase
    {
        Task ExecuteAsync(Guid categoryId, UpdateCategoryRequest request);
    }
}
