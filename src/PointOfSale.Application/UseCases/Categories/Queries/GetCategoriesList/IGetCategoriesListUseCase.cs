using PointOfSale.Application.DTOs.Response.Product;

namespace PointOfSale.Application.UseCases.Categories.Queries.GetCategoriesList
{
    public interface IGetCategoriesListUseCase
    {
        Task<IReadOnlyCollection<CategoryResponse>> ExecuteAsync();
    }
}
