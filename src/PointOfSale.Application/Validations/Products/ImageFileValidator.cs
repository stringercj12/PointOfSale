using FluentValidation;
using PointOfSale.Application.DTOs.Request.Product;

namespace PointOfSale.Application.Validations.Products;

public class ImageFileValidator : AbstractValidator<ProductPhotoListRequest>
{
    private const long MaxFileSizeInBytes = 2 * 1024 * 1024;
    private static readonly string[] AllowedContentTypes = ["image/jpeg", "image/jpg", "image/png", "image/webp"];

    public ImageFileValidator()
    {
        RuleFor(file => file.Photo).NotNull().WithMessage("An image file is required.");

        RuleFor(file => file.Photo.ContentType)
            .NotNull()
            .Must(contentType => AllowedContentTypes.Contains(contentType))
            .WithMessage("Unsupported file type. Only JPEG, PNG, and GIF are allowed.");

        // // 3. Optional: Validate Max File Size (e.g., Max 5MB)
        // RuleFor(file => file.Length)
        //     .LessThanOrEqualTo(5 * 1024 * 1024)
        //     .WithMessage("Image size must be less than 5MB.");
    }
}