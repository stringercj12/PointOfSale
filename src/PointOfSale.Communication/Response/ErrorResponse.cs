namespace PointOfSale.Communication.Response
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public List<string> ErrorMessages { get; set; } = [];

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
    }
}
