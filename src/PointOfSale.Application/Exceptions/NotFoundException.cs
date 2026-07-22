using System.Net;

namespace PointOfSale.Application.Exceptions;

public class NotFoundException : PointOfSaleBaseException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}