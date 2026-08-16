using FluentValidation;
using Microsoft.AspNetCore.Http;
using PointOfSale.Application.DTOs.Request.Product;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.UseCases.Common.Interfaces;
using PointOfSale.Application.Validations;
using PointOfSale.Domain.Entities;
using PointOfSale.Domain.Exceptions;
using PointOfSale.Domain.Repositories;
using System.Collections.Concurrent;
using System.Security.Cryptography;

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
                      ?? throw new NotFoundException($"Produto não encontrado.");

        var uploadedFilePaths = new ConcurrentBag<string>();
        var newAttachments = new List<(Attachment, bool, int)>();

        try
        {

            foreach (var photoDto in request.ProductPhotos)
            {
                if (await AttachmentAlreadyExistsAsync(product, photoDto.Photo))
                    throw new DomainException(
                        $"A imagem '{photoDto.Photo.FileName}' já foi adicionada ao produto.");

                var extension = Path.GetExtension(photoDto.Photo.FileName);
                var storedName = $"{Guid.NewGuid()}{extension}";
                var path = $"products/{product.Id}/{storedName}";

                var hash = await GetFileHashAsync(photoDto.Photo);

                var attachment = Attachment.Create(
                    path,
                    storedName,
                    photoDto.Photo.FileName,
                    extension,
                    photoDto.Photo.ContentType,
                    photoDto.Photo.Length,
                    hash
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
        catch (Exception ex)
        {

            if (!uploadedFilePaths.IsEmpty)
            {
                var deleteTasks = uploadedFilePaths.Select(path => _storageService.DeleteAsync(path));
                await Task.WhenAll(deleteTasks);
            }

            throw new BusinessException(ex.Message);
        }

    }

    private static async Task<string> GetFileHashAsync(IFormFile file)
    {
        await using var stream = file.OpenReadStream();

        using var sha256 = SHA256.Create();

        var hashBytes = await sha256.ComputeHashAsync(stream);

        return Convert.ToHexString(hashBytes);
    }

    private async Task<bool> AttachmentAlreadyExistsAsync(Product product, IFormFile file)
    {
        await using var stream = file.OpenReadStream();

        using var sha256 = SHA256.Create();

        var hashBytes = await sha256.ComputeHashAsync(stream);

        var hash = Convert.ToHexString(hashBytes);

        return product.ProductAttachments.Any(pa =>
            pa.Attachment.Hash == hash);
    }

}