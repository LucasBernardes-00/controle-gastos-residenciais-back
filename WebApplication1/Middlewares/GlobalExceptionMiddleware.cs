using Domain.Exceptions;
using System.Net;
using System.Text.Json;
using WebApplication1.DTOs.Response;

namespace WebApplication1.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Método Invoke para capturar excepciones globalmente para cada requisição
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InputException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var response = new ErrorResponse(400, ex.Message, ex.Errors);

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                ArgumentException or InvalidOperationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = statusCode;

            var response = new ErrorResponse(statusCode, exception.Message, new List<string>());

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
