using FluentValidation;
using PointOfSale.Application.DTOs.Request.Product;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Validations;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Products.Commands.UpdateProductCategories;

public class UpdateProductCategoriesUseCase : IUpdateProductCategoriesUseCase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateProductCategoriesRequest> _validator;

    public UpdateProductCategoriesUseCase(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork,
        IValidator<UpdateProductCategoriesRequest> validator)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task ExecuteAsync(UpdateProductCategoriesRequest request)
    {
        _validator.ValidateAndThrowCustom(request);

        var distinctCategoryIds = request.CategoryIds.Distinct().ToList();

        var product = await _productRepository.GetByIdAsync(request.ProductId) ?? throw new NotFoundException($"Produto não encontrado.");

        var categories = await _categoryRepository.GetByIds(distinctCategoryIds);

        if (categories.Count != distinctCategoryIds.Count)
        {
            var foundIds = categories.Where(c => c.Active).Select(c => c.Id);
            var missingIds = distinctCategoryIds.Except(foundIds);

            throw new BusinessException($"As seguintes categorias não foram encontradas: {string.Join(", ", missingIds)}");
        }

        product.UpdateCategories(categories);

        _productRepository.Update(product);

        await _unitOfWork.SaveChangesAsync();
    }
}