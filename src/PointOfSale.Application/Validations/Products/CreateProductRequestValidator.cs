using FluentValidation;
using PointOfSale.Application.DTOs.Request.Product;

namespace PointOfSale.Application.Validations.Products;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(150).WithMessage("O nome deve ter no máximo 150 caracteres.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("O preço do produto deve ser maior que zero.");

        RuleFor(x => x.SKU)
            .NotEmpty().WithMessage("O SKU do produto é obrigatório.")
            .MaximumLength(50).WithMessage("O SKU deve ter no máximo 50 caracteres.");

        RuleFor(x => x.CategoryIds)
            .NotEmpty().WithMessage("Informe pelo menos uma categoria para o produto.");
    }
}