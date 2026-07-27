using FluentValidation;
using PointOfSale.Application.DTOs.Request.Product;
using PointOfSale.Application.DTOs.Response.Product;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Mappers;
using PointOfSale.Application.Validations;
using PointOfSale.Domain.Entities;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Products.Commands.CreateProduct;

public class CreateProductUseCase : ICreateProductUseCase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateProductRequest> _validator;

    public CreateProductUseCase(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateProductRequest> validator)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<ProductResponse> ExecuteAsync(CreateProductRequest request)
    {
        _validator.ValidateAndThrowCustom(request);

        var existsWithSku = await _productRepository.ExistsBySkuAsync(request.SKU);
        if (existsWithSku) throw new BusinessException($"Já existe um produto cadastrado com o SKU '{request.SKU}'.");

        var product = Product.Create(
            request.Name,
            request.Description,
            request.Price,
            request.SKU
        );

        if (request.CategoryIds != null && request.CategoryIds.Count > 0)
        {
            var distinctCategoryIds = request.CategoryIds.Distinct().ToList();
            var categories = await _categoryRepository.GetByIds(distinctCategoryIds);

            if (categories.Count != distinctCategoryIds.Count)
            {
                var foundIds = categories.Select(c => c.Id);
                var missingIds = distinctCategoryIds.Except(foundIds);
                throw new NotFoundException($"As seguintes categorias não foram encontradas: {string.Join(", ", missingIds)}");
            }

            product.UpdateCategories(categories.Select(c => c.Id).ToList());
        }

        _productRepository.Add(product);
        await _unitOfWork.SaveChangesAsync();

        return product.ToResponse();
    }
}