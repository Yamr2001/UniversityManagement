using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using UniversityManagement.Shared.Domain;
using ValidationException = UniversityManagement.Shared.Domain.ValidationException;

namespace UniversityManagement.Shared.Middlewares
{
    public sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private async static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                errors = GetErrors(exception)
            };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => (int)HttpStatusCode.BadRequest,
                _ => StatusCodes.Status500InternalServerError,

            };
        }

        private static string GetTitle(Exception exception)
        {
            return exception switch
            {
                ValidationException => "Validation Failure",
                _ => "Internal Server Error"
            };
        }

        private static List<string> GetErrors(Exception exception)
        {
            var errorMessages = new List<string>();

            if (exception is ValidationException validationException)
            {
                foreach (var error in validationException.Errors)
                {
                    errorMessages.AddRange(error);
                }
            }
            else if (exception is ValidationException newValidationException)
            {
                errorMessages.AddRange(newValidationException.Errors);
            }

            return errorMessages.Count > 0 ? errorMessages : [];
        }
    }
}
