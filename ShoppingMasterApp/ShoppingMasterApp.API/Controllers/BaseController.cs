using Microsoft.AspNetCore.Mvc;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult ApiResponse(object result = null, bool isSuccess = true, string message = null)
        {
            return Ok(new ApiResponse<object>(statusCode: 200, isSuccess: isSuccess, data: result, errorMessage: message));
        }

        protected IActionResult ApiError(string message, int statusCode = 400)
        {
            return StatusCode(statusCode, new ApiResponse<object>(statusCode: statusCode, isSuccess: false, data: null, errorMessage: message));
        }

    }
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public ApiResponse(int statusCode, bool isSuccess, T data = default(T), string errorMessage = null)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
        }
    }
}
