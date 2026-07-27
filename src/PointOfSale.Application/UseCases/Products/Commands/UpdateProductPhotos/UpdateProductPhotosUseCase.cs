using FluentValidation;
using PointOfSale.Application.DTOs.Request.Product;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.UseCases.Common.Interfaces;
using PointOfSale.Application.Validations;
using PointOfSale.Domain.Entities;
using PointOfSale.Domain.Repositories;
using System.Collections.Concurrent;

namespace PointOfSale.Application.UseCases.Products.Commands.UpdateProductPhotos;

public class UpdateProductPhotosUseCase : IUpdateProductPhotosUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IStorageService _storageService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateProductPhotosRequest> _validator;

    public UpdateProductPhotosUseCase(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IValidator<UpdateProductPhotosRequest> validator,
        IStorageService storageService,
        IAttachmentRepository attachmentRepository)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _storageService = storageService;
        _attachmentRepository = attachmentRepository;
    }

    public async Task ExecuteAsync(UpdateProductPhotosRequest request)
    {
        _validator.ValidateAndThrowCustom(request);

        var product = await _productRepository.GetForUpdateAsync(request.ProductId)
                      ?? throw new NotFoundException($"Produto com ID '{request.ProductId}' não foi encontrado.");

        var uploadedFilePaths = new ConcurrentBag<string>();
        var newAttachments = new List<(Attachment, bool, int)>();

        try
        {

            foreach (var photoDto in request.ProductPhotos)
            {
                var extension = Path.GetExtension(photoDto.Photo.FileName);
                var storedName = $"{Guid.NewGuid()}{extension}";
                var path = $"products/{product.Id}/{storedName}";

                var attachment = Attachment.Create(
                    path,
                    storedName,
                    photoDto.Photo.FileName,
                    extension,
                    photoDto.Photo.ContentType,
                    photoDto.Photo.Length
                );


                await _attachmentRepository.Add(attachment);

                newAttachments.Add((attachment, photoDto.IsPrimary, photoDto.DisplayOrder));

                await using var stream = photoDto.Photo.OpenReadStream();

                await _storageService.UploadAsync(
                    stream,
                    path,
                    photoDto.Photo.ContentType);

                uploadedFilePaths.Add(path);
            }


            product.UpdateAttachments(newAttachments);

            await _unitOfWork.SaveChangesAsync();
        }
        catch(Exception ex)
        {

            if (!uploadedFilePaths.IsEmpty)
            {
                var deleteTasks = uploadedFilePaths.Select(path => _storageService.DeleteAsync(path));
                await Task.WhenAll(deleteTasks);
            }

            throw new BusinessException(ex.Message);
        }

    }
}