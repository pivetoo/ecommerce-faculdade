using Ecommerce.API.Middlewares.Helpers;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace Ecommerce.API.Middlewares
{
    public class ErrorHandlingMiddleware : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";

            switch (exception)
            {
                case AppException e:
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case KeyNotFoundException e:
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                default:
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = exception?.Message });
            await httpContext.Response.WriteAsync(result, cancellationToken);

            return true;
        }
    }
}
