using FluentValidation;
using PointOfSale.Application.DTOs.Request.Tables;

namespace PointOfSale.Application.Validations.Tables
{
    public class CreateTableRequestValidator : AbstractValidator<CreateTableRequest>
    {

        public CreateTableRequestValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("O nome da mesa é obrigatório.")
               .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");


            RuleFor(x => x.Number)
               .GreaterThan(0).WithMessage("O número da mesa deve ser maior que zero");

            RuleFor(x => x.Capacity)
               .GreaterThan(0).WithMessage("A quantidade de lugares deve ser maior que zero");
        }
    }
}
