using FluentValidation;
using PointOfSale.Application.Exceptions;

namespace PointOfSale.Application.Validations;

public static class FluentValidationExtensions
{
    public static void ValidateAndThrowCustom<T>(this IValidator<T> validator, T instance)
    {
        var resultValidator = validator.Validate(instance);

        if (!resultValidator.IsValid)
        {
            var result = resultValidator.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(result);
        }
    }
}