using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using WebApplication1.DTOs.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InputException ex) // Captura a validação manual do UseCase
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400

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

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
