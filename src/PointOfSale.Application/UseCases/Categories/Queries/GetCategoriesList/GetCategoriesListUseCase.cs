using PointOfSale.Application.DTOs.Response.Product;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListUseCase : IGetCategoriesListUseCase
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesListUseCase(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IReadOnlyCollection<CategoryResponse>> ExecuteAsync()
        {
            var categories = await _categoryRepository.GetAll();

            return categories.Select(category => new CategoryResponse(
                category.Id,
                category.Name,
                category.Description,
                category.Active,
                category.CreatedAt,
                category.UpdatedAt
            )).ToList().AsReadOnly();
        }
    }
}
