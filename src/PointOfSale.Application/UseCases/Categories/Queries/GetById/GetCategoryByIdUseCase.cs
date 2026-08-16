using PointOfSale.Application.DTOs.Response.Category;
using PointOfSale.Application.Exceptions;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Categories.Queries.GetById
{
    public class GetCategoryByIdUseCase : IGetCategoryByIdUseCase
    {

        private readonly ICategoryRepository _categoyRepository;

        public GetCategoryByIdUseCase(ICategoryRepository categoyRepository)
        {
            _categoyRepository = categoyRepository;
        }

        public async Task<CategoryResponse> ExecuteAsync(Guid categoryId)
        {

            var category = await _categoyRepository.GetById(categoryId) ?? throw new NotFoundException("Categoria não encontrada");

            return new CategoryResponse(category.Id, category.Name, category.Description, category.Active);

        }
    }
}
