using FluentValidation;
using PointOfSale.Application.DTOs.Request.Product;

namespace PointOfSale.Application.Validations.Products;

public class UpdateProductCategoriesRequestValidator : AbstractValidator<UpdateProductCategoriesRequest>
{
    public UpdateProductCategoriesRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("O Id do produto é obrigatório.");

        RuleFor(x => x.CategoryIds)
            .NotEmpty().WithMessage("Informe pelo menos uma categoria para o produto.");
    }
}