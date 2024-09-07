using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Domain.Enums;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult ApiSuccess<T>(T result, string message = null)
        {
            return Ok(new ApiResponse<T>(statusCode: 200, isSuccess: true, data: result, errorMessage: null, responseStatus: ResponseStatus.Success.ToString()));
        }

        // Error Response with customizable status code
        protected IActionResult ApiError(string message, int statusCode = 400, ResponseStatus responseStatus = ResponseStatus.Error)
        {
            return StatusCode(statusCode, new ApiResponse<object>(statusCode, false, null, message, responseStatus.ToString()));
        }

        // Not Found Response
        protected IActionResult ApiNotFound(string message = "Resource not found")
        {
            return ApiError(message, 404, ResponseStatus.NotFound);
        }

        // Unauthorized Response
        protected IActionResult ApiUnauthorized(string message = "Unauthorized access")
        {
            return ApiError(message, 401, ResponseStatus.Unauthorized);
        }

        // Validation Error Response
        protected IActionResult ApiValidationError(IEnumerable<string> errors)
        {
            var validationResponse = new ApiResponse<object>(statusCode: 400, isSuccess: false, errorMessage: "Validation error", responseStatus: ResponseStatus.ValidationError.ToString())
            {
                Data = new { Errors = errors }
            };
            return BadRequest(validationResponse);
        }

        // Server Error Response
        protected IActionResult ApiServerError(string message = "Internal server error")
        {
            return ApiError(message, 500, ResponseStatus.ServerError);
        }
    }

    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public string ResponseStatus { get; set; }

        public ApiResponse(int statusCode, bool isSuccess, T data = default, string errorMessage = null, string responseStatus = "Success")
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
            ResponseStatus = responseStatus;
        }
    }
}
