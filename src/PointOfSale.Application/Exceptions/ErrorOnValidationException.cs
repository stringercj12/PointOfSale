using System.Net;

namespace PointOfSale.Application.Exceptions;

public class ErrorOnValidationException : PointOfSaleBaseException
{
    private readonly List<string> _errors;

    public ErrorOnValidationException(List<string> messages) : base(string.Empty)
    {
        _errors = messages;
    }

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors()
    {
        return _errors;
    }
}