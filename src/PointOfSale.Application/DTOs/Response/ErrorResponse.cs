namespace PointOfSale.Application.DTOs.Response;

public class ErrorResponse
{
    public ErrorResponse(string errorMessage, int statusCode)
    {
        StatusCode = statusCode;
        ErrorMessages = [errorMessage];
    }

    public ErrorResponse(List<string> errors, int statusCode)
    {
        StatusCode = statusCode;
        ErrorMessages = errors;
    }

    public int StatusCode { get; set; }
    public List<string> ErrorMessages { get; set; } = [];
}