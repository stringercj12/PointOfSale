using System.Net;

namespace PointOfSale.Application.Exceptions;

public class DuplicateResourceException : PointOfSaleBaseException
{
    public DuplicateResourceException(string message) : base(message)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.Conflict;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}