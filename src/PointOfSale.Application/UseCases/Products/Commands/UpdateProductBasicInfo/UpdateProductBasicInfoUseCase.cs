using FluentValidation;
using PointOfSale.Application.DTOs.Request.Product;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Validations;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Products.Commands.UpdateProductBasicInfo;

public class UpdateProductBasicInfoUseCase : IUpdateProductBasicInfoUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateProductBasicInfoRequest> _validator;

    public UpdateProductBasicInfoUseCase(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IValidator<UpdateProductBasicInfoRequest> validator)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task ExecuteAsync(Guid productId, UpdateProductBasicInfoRequest request)
    {
        _validator.ValidateAndThrowCustom(request);

        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null)
            throw new BusinessException("Não foi encontrado nenhum produto com esse id.");

        product.UpdateProduct(
            request.Name,
            request.Description,
            request.Price
        );

        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();
    }
}