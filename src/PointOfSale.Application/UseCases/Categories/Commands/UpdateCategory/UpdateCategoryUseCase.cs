using PointOfSale.Application.DTOs.Request.Categories;
using PointOfSale.Application.Exceptions;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Categories.Commands.UpdateCategory.UpdateCategory
{
    public class UpdateCategoryUseCase : IUpdateCategoryUseCase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryUseCase(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Guid categoryId, UpdateCategoryRequest request)
        {

            var category = await _categoryRepository.GetForUpdateAsync(categoryId) ?? throw new NotFoundException("Categoria não encontrada");

            category.UpdateCategory(request.Name, request.Description, request.Active);

            _categoryRepository.Update(category);

           await _unitOfWork.SaveChangesAsync();

        }
    }
}
