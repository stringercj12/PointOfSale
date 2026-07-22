using FluentValidation;
using PointOfSale.Application.DTOs.Request.Product;

namespace PointOfSale.Application.Validations.Products;

public class UpdateProductBasicInfoRequestValidator : AbstractValidator<UpdateProductBasicInfoRequest>
{
    public UpdateProductBasicInfoRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(150).WithMessage("O nome deve ter no máximo 150 caracteres.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("O preço do produto deve ser maior que zero.");
    }
}