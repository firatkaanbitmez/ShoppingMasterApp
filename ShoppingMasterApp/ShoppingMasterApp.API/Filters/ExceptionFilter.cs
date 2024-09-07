using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ShoppingMasterApp.API.Controllers;
using ShoppingMasterApp.Domain.Exceptions;
using System;
using System.Net;

namespace ShoppingMasterApp.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Log the exception details
            _logger.LogError(context.Exception, "Unhandled exception occurred.");

            // Determine the type of exception and set the response accordingly
            var response = context.Exception switch
            {
                InvalidInputException => new ApiResponse<object>(statusCode: (int)HttpStatusCode.BadRequest, isSuccess: false, data: null, errorMessage: context.Exception.Message),
                UnauthorizedAccessException => new ApiResponse<object>(statusCode: (int)HttpStatusCode.Unauthorized, isSuccess: false, data: null, errorMessage: "Unauthorized access"),
                NotFoundException => new ApiResponse<object>(statusCode: (int)HttpStatusCode.NotFound, isSuccess: false, data: null, errorMessage: "Resource not found"),
                ValidationException => new ApiResponse<object>(statusCode: (int)HttpStatusCode.BadRequest, isSuccess: false, data: null, errorMessage: "Validation error"),
                _ => new ApiResponse<object>(statusCode: (int)HttpStatusCode.InternalServerError, isSuccess: false, data: null, errorMessage: "An internal server error occurred.")
            };

            // Set the context result to return a standardized error response
            context.Result = new JsonResult(response)
            {
                StatusCode = response.StatusCode
            };

            // Mark the exception as handled
            context.ExceptionHandled = true;
        }

    }
}
