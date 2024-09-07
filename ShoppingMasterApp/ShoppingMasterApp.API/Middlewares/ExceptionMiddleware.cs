using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShoppingMasterApp.Domain.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex.Message}, StackTrace: {ex.StackTrace}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = exception switch
            {
                InvalidInputException => new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid input provided."
                },
                UnauthorizedAccessException => new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "You are not authorized to access this resource."
                },
                _ => new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Internal Server Error."
                }
            };

            return context.Response.WriteAsync(response.ToString());
        }

    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
