using FluentValidation;
using PointOfSale.Application.DTOs.Request.Users;

namespace PointOfSale.Application.Validations.Users;

public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
{
    public UserUpdateRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("O cargo (Role) fornecido é inválido.");
    }
}