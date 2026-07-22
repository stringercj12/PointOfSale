using FluentValidation;
using PointOfSale.Application.DTOs.Request.Categories;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Validations;
using PointOfSale.Domain.Entities;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Categories.Commands.CreateCategory;

public class CreateCategoryUseCase : ICreateCategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateCategoryRequest> _validator;

    public CreateCategoryUseCase(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateCategoryRequest> validator)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }


    public async Task ExecuteAsync(CreateCategoryRequest request)
    {
        _validator.ValidateAndThrowCustom(request);

        var existsByName = await _categoryRepository.ExistsByName(request.Name);

        if (existsByName)
            throw new BusinessException($"Category with name {request.Name} already exists");

        var category = Category.Create(request.Name, request.Description);

        await _categoryRepository.Add(category);
        await _unitOfWork.SaveChangesAsync();
    }
}