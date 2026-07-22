using System.Net;

namespace PointOfSale.Application.Exceptions;

public class BusinessException : PointOfSaleBaseException
{
    public BusinessException(string message) : base(message)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}