using Microsoft.AspNetCore.Mvc;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HandleResponse<T>(T result)
        {
            if (result is bool boolResult && !boolResult)
            {
                return NotFound(new ApiResponse<T>
                {
                    Success = false,
                    Message = "Record not found",
                    StatusCode = 404,
                    Data = result
                });
            }

            if (result == null)
            {
                return NotFound(new ApiResponse<T>
                {
                    Success = false,
                    Message = "No data found",
                    StatusCode = 404,
                    Data = result
                });
            }

            return Ok(new ApiResponse<T>
            {
                Success = true,
                Message = "Request successful",
                StatusCode = 200,
                Data = result
            });
        }


        protected IActionResult HandleErrorResponse(string errorMessage, int statusCode = 400)
        {
            return StatusCode(statusCode, new ApiResponse<string>
            {
                Success = false,
                Message = errorMessage,
                StatusCode = statusCode,
                Data = null
            });
        }

        protected IActionResult HandleExceptionResponse(Exception ex)
        {
            // Loglama işlemi burada yapılabilir
            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Message = $"An internal server error occurred: {ex.Message}",
                StatusCode = 500,
                Data = null
            });
        }
    }

    // ApiResponse sınıfı ile API yanıtlarını standart hale getiriyoruz
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}
