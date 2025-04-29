using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using UniversityManagement.Shared.Domain;
using static UniversityManagement.Shared.Domain.CustomValidationException;
using CustomValidationException = UniversityManagement.Shared.Domain.CustomValidationException;

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
                CustomValidationException => (int)HttpStatusCode.BadRequest,
                _ => StatusCodes.Status500InternalServerError,

            };
        }

        private static string GetTitle(Exception exception)
        {
            return exception switch
            {
                CustomValidationException => "unExcepected Error",
                ValidationExceptionValidator => "Validation Failure",
                _ => "Internal Server Error"
            };
        }

        private static List<string> GetErrors(Exception exception)
        {
            var errorMessages = new List<string>();

            if (exception is CustomValidationException validationException)
            {
                foreach (var error in validationException.Errors)
                {
                    errorMessages.AddRange(error);
                }
            }
            else if (exception is ValidationExceptionValidator newValidationException)
            {
                errorMessages.AddRange(newValidationException.ErrorMessages);
            }

            return errorMessages.Count > 0 ? errorMessages : [];
        }
    }
}
