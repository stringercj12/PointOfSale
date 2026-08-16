using PointOfSale.Application.Exceptions;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Categories.Commands.DisableCategory
{
    public class DisableCategoryUseCase : IDisableCategoryUseCase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DisableCategoryUseCase(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(Guid categoryId)
        {
            var category = await _categoryRepository.GetForUpdateAsync(categoryId)
                      ?? throw new NotFoundException($"Categoria não encontrado.");


            if (!category.Active)
                throw new BusinessException("A Categoria já esta desativada");


                category.ToggleActiveStatus();

            await _unitOfWork.SaveChangesAsync();

        }
    }
}
