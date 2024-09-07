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
            // Log full details of the exception
            _logger.LogError(context.Exception, "Unhandled exception occurred: {Message}, StackTrace: {StackTrace}",
                             context.Exception.Message, context.Exception.StackTrace);

            var response = context.Exception switch
            {
                InvalidInputException => new ApiResponse<object>(statusCode: (int)HttpStatusCode.BadRequest, isSuccess: false, data: null, errorMessage: context.Exception.Message),
                UnauthorizedAccessException => new ApiResponse<object>(statusCode: (int)HttpStatusCode.Unauthorized, isSuccess: false, data: null, errorMessage: "Unauthorized access"),
                NotFoundException => new ApiResponse<object>(statusCode: (int)HttpStatusCode.NotFound, isSuccess: false, data: null, errorMessage: "Resource not found"),
                ValidationException => new ApiResponse<object>(statusCode: (int)HttpStatusCode.BadRequest, isSuccess: false, data: null, errorMessage: "Validation error"),
                _ => new ApiResponse<object>(statusCode: (int)HttpStatusCode.InternalServerError, isSuccess: false, data: null, errorMessage: "An internal server error occurred.")
            };

            context.Result = new JsonResult(response)
            {
                StatusCode = response.StatusCode
            };

            context.ExceptionHandled = true;
        }


    }
}
