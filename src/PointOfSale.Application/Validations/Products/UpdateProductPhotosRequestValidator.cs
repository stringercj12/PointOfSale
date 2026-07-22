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
            .NotEmpty().WithMessage("Informe pelo menos uma imagem do produto.");

        RuleForEach(x => x.ProductPhotos)
            .SetValidator(new ImageFileValidator());
    }
}