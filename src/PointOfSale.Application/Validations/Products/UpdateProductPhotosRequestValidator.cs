using FluentValidation;
using PointOfSale.Application.DTOs.Request.Product;

namespace PointOfSale.Application.Validations.Products;

public class UpdateProductPhotosRequestValidator : AbstractValidator<UpdateProductPhotosRequest>
{
    public UpdateProductPhotosRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("O Id do produto é obrigatório.");

        RuleFor(x => x.ProductPhotos)
            .NotEmpty().WithMessage("O Id ddsdsdsddo produto é obrigatório.");

        RuleForEach(x => x.ProductPhotos)
            .SetValidator(new ImageFileValidator());
    }
}