using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UniversityManagement.Shared.Middlewares
{
    public sealed class GlobalException : IExceptionHandler
    {
        private readonly ILogger<GlobalException> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public GlobalException(ILogger<GlobalException> logger, IHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, exception.Message);
            var ProblemDetails = CreateProblemDetails(httpContext, exception);
            var jsonResult = new JsonResult(ProblemDetails)
            {
                StatusCode = ProblemDetails.Status,
                ContentType = "application/problem+json"
            };
            httpContext.Response.StatusCode = ProblemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(ProblemDetails, cancellationToken);

            return true;
        }



        private ProblemDetails CreateProblemDetails(HttpContext httpContext, Exception exception)
        {
            var statusCode = exception switch
            {
                ArgumentNullException => HttpStatusCode.BadRequest,
                ArgumentException => HttpStatusCode.BadRequest,
                InvalidOperationException => HttpStatusCode.Conflict,
                NotImplementedException => HttpStatusCode.NotImplemented,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            };

            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred",
                Status = (int)statusCode,
                Detail = _hostEnvironment.IsDevelopment()
                    ? exception.Message
                    : "An unexpected error occurred. Please try again later.",
                Instance = httpContext.Request.Path,
                Type = $"https://httpstatuses.com/{(int)statusCode}"
            };

            if (_hostEnvironment.IsDevelopment())
            {
                problemDetails.Extensions.Add("stackTrace", exception.StackTrace);
                problemDetails.Extensions.Add("innerException", exception.InnerException?.Message);
            }

            return problemDetails;

        }
    }
}
