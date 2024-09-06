using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.API.Controllers;

namespace ShoppingMasterApp.API.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Validation error",
                    StatusCode = 400,
                    Data = context.ModelState
                });
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
