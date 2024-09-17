using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ShoppingMasterApp.API.Controllers;
using ShoppingMasterApp.Domain.Enums;
using System;
using System.Net;
using ResponseStatus = ShoppingMasterApp.Domain.Enums.ResponseStatus;

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
            // Log exception details
            _logger.LogError($"Something went wrong: {context.Exception}");

            var response = new ApiResponse<object>
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                IsSuccess = false,
                ErrorMessage = "An unexpected error occurred. Please try again later.",
                ResponseStatus = ResponseStatus.ServerError.ToString(),
                Data = null
            };

            if (context.Exception is ArgumentException || context.Exception is ArgumentNullException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.ErrorMessage = context.Exception.Message;
                response.ResponseStatus = Domain.Enums.ResponseStatus.ValidationError.ToString();
            }
            else if (context.Exception is KeyNotFoundException)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.ErrorMessage = context.Exception.Message;
                response.ResponseStatus = ResponseStatus.NotFound.ToString();
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response.ErrorMessage = "Access denied.";
                response.ResponseStatus = ResponseStatus.Unauthorized.ToString();
            }

            context.Result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };

            context.ExceptionHandled = true; // Exception is handled
        }
    }
}
