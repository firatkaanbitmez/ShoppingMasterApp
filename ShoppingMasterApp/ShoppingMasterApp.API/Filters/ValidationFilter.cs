using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoppingMasterApp.API.Controllers;
using ShoppingMasterApp.Domain.Enums;
using System.Linq;
using System.Collections.Generic;

namespace ShoppingMasterApp.API.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // Her bir alanın hatasını detaylandırarak liste halinde topluyoruz
                var errors = context.ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .Select(ms => new ValidationErrorDetail
                    {
                        FieldName = ms.Key,
                        ErrorMessages = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    })
                    .ToList();

                var validationResponse = new ApiResponse<object>
                {
                    StatusCode = 400,
                    IsSuccess = false,
                    ErrorMessage = "Validation error occurred. Please check the provided details.",
                    ResponseStatus = ResponseStatus.ValidationError.ToString(),
                    Data = new { Errors = errors }
                };

                // Detaylı bir hata döndürülmesi
                context.Result = new BadRequestObjectResult(validationResponse);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }

    // Hata detaylarını daha iyi göstermek için özel bir yapı oluşturuyoruz
    public class ValidationErrorDetail
    {
        public string FieldName { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
