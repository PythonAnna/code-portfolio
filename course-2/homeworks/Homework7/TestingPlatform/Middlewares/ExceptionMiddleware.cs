using System;
using TestingPlatform.Infrastructure.Exceptions;

namespace TestingPlatform.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(InvalidOperationException exc)
            {
                await HandleExeptionAsync(context, StatusCodes.Status400BadRequest, exc.Message);
            }
            catch (ArgumentException exc)
            {
                await HandleExeptionAsync(context, StatusCodes.Status400BadRequest, exc.Message);
            }
            catch (EntityNotFoundException exc)
            {
                await HandleExeptionAsync(context, StatusCodes.Status404NotFound, exc.Message);
            }
            catch (Exception exc)
            {
                await HandleExeptionAsync(context, StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        private async Task HandleExeptionAsync(HttpContext context, int ststusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ststusCode;
            await context.Response.WriteAsJsonAsync(new { message = message });
        }
    }
}
