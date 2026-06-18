using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PointOfSale.Application.DTOs.Response;
using PointOfSale.Application.Exceptions;

namespace PointOfSale.API.Middlewares
{
    public class GlobalExceptionHandlerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is PointOfSaleBaseException)
            {
                HandleProjectException(context);
            }
            else
            {
                ThrowUnkowError(context);
            }

        }

        public void HandleProjectException(ExceptionContext context)
        {
            var pointofSaleException = (PointOfSaleBaseException)context.Exception;
            var errorResponse = new ErrorResponse(pointofSaleException.GetErrors(), pointofSaleException.StatusCode);

            context.HttpContext.Response.StatusCode = pointofSaleException.StatusCode;
            context.Result = new ObjectResult(errorResponse);

        }

        private void ThrowUnkowError(ExceptionContext context)
        {
            var errors = new List<string>()
            {
                "UNKNOW_ERROR",
                context.Exception.ToString()
            };

            var errorResponse = new ErrorResponse(errors, StatusCodes.Status500InternalServerError);

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
