using System.Net;

namespace PointOfSale.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }

    public int StatusCode => (int)HttpStatusCode.BadRequest;

    public List<string> GetErrors()
    {
        return [Message];
    }
}