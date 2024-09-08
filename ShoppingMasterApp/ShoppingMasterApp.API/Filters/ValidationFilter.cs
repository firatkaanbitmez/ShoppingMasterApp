using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoppingMasterApp.API.Controllers;
using ShoppingMasterApp.Domain.Enums;
using System.Linq;

namespace ShoppingMasterApp.API.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var validationResponse = new ApiResponse<object>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    ErrorMessage = "Validation error",
                    ResponseStatus = ResponseStatus.ValidationError.ToString(),
                    Data = new { Errors = errors }
                };

                context.Result = new BadRequestObjectResult(validationResponse);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
