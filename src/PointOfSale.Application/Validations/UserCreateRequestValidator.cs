using FluentValidation;
using PointOfSale.Communication.Request.User;

namespace PointOfSale.Application.Validations
{
    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("O nome é obrigatório.")
              .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O formato do e-mail inválido.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("O nome de usuário é obrigatório.")
              .MaximumLength(50).WithMessage("O nome de usuário deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(5).WithMessage("A senha deve ter no mínimo 5 caracteres.");

            RuleFor(x => x.Role)
               .IsInEnum().WithMessage("O cargo (Role) fornecido é inválido.");

            //RuleFor(x => x.ProfileImage)
            //    .Must(file => file.Length > 0).WithMessage("O arquivo de imagem não pode estar vazio.")
            //    .Must(file => file.ContentType.StartsWith("image/")).WithMessage("O arquivo deve ser uma imagem válida.");
        }
    }
}
