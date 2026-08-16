using PointOfSale.Application.DTOs.Response.Category;

namespace PointOfSale.Application.UseCases.Categories.Queries.GetById
{
    public interface IGetCategoryByIdUseCase
    {
        Task<CategoryResponse> ExecuteAsync(Guid categoryId);
    }
}
