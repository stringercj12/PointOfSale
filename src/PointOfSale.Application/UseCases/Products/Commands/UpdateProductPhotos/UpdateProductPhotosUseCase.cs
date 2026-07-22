using FluentValidation;
using PointOfSale.Application.DTOs.Request.Product;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.UseCases.Common.Interfaces;
using PointOfSale.Application.Validations;
using PointOfSale.Domain.Entities;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Application.UseCases.Products.Commands.UpdateProductPhotos;

public class UpdateProductPhotosUseCase : IUpdateProductPhotosUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly IStorageService _storageService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateProductPhotosRequest> _validator;

    public UpdateProductPhotosUseCase(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IValidator<UpdateProductPhotosRequest> validator,
        IStorageService storageService)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _storageService = storageService;
    }

    public async Task ExecuteAsync(UpdateProductPhotosRequest request)
    {
        _validator.ValidateAndThrowCustom(request);

        var product = await _productRepository.GetByIdAsync(request.ProductId)
                      ?? throw new NotFoundException($"Produto com ID '{request.ProductId}' não foi encontrado.");

        var uploadedFilePaths = new List<string>();

        try
        {
            foreach (var photoDto in request.ProductPhotos)
            {
                var extension = Path.GetExtension(photoDto.FileName);
                var storedName = $"{Guid.NewGuid()}{extension}";
                var path = $"products/{product.ProductId}/{storedName}";
                var stream = photoDto.OpenReadStream();

                var attachment = Attachment.Create(
                    path,
                    storedName,
                    photoDto.FileName,
                    extension,
                    photoDto.ContentType,
                    stream.Length
                );

                product.AddAttachment(attachment);

                await _storageService.UploadAsync(stream, path, photoDto.ContentType);
                uploadedFilePaths.Add(path);
            }

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }
        catch
        {
            foreach (var path in uploadedFilePaths) await _storageService.DeleteAsync(path);

            throw;
        }
    }
}