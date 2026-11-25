using System;
using TestingPlatform.Infrastructure.Exceptions;

namespace TestingPlatform.API.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch(Exception exception)
            {
                logger.LogError(exception, $"Exception: {exception.Message}");
                var errorCodeResponse = exception switch
                {
                    InvalidOperationException exc =>
                        new CodeAndMessage(StatusCodes.Status400BadRequest, exc.Message),
                    ArgumentException exc =>
                        new CodeAndMessage(StatusCodes.Status400BadRequest, exc.Message),
                    EntityNotFoundException exc =>
                        new CodeAndMessage(StatusCodes.Status404NotFound, exc.Message),
                    _ => new CodeAndMessage(StatusCodes.Status500InternalServerError, "Internal server error")
                };
                await HandleExeptionAsync(httpContext, errorCodeResponse);
            }
        }
        private async Task HandleExeptionAsync(HttpContext context, CodeAndMessage errorCodeResponse)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorCodeResponse.HttpStatusCode;
            await context.Response.WriteAsJsonAsync(new { errorCodeResponse.Message });
        }

        private record struct CodeAndMessage(int HttpStatusCode, string Message);
    }
}
