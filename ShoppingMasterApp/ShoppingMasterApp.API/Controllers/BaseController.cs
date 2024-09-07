using Microsoft.AspNetCore.Mvc;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Standardized success response.
        /// </summary>
        /// <typeparam name="T">Type of the result</typeparam>
        /// <param name="result">Result object to return</param>
        /// <param name="statusCode">Optional status code, defaults to 200 (OK)</param>
        /// <returns>Standardized success response</returns>
        protected IActionResult ApiResponse<T>(T result, int statusCode = 200)
        {
            if (result == null)
            {
                return NotFound(new ApiResponse<T>(statusCode: 404, isSuccess: false, errorMessage: "Resource not found"));
            }

            return StatusCode(statusCode, new ApiResponse<T>(statusCode: statusCode, isSuccess: true, data: result));
        }

        /// <summary>
        /// Standardized error response for validation or processing errors.
        /// </summary>
        /// <param name="errorMessage">The error message to display</param>
        /// <param name="statusCode">Optional status code, defaults to 400 (BadRequest)</param>
        /// <param name="errorDetails">Additional error details for better error visibility</param>
        /// <returns>Standardized error response</returns>
        protected IActionResult ApiError(string errorMessage, int statusCode = 400, object errorDetails = null)
        {
            // Since we removed IWebHostEnvironment, no dev-safe message handling here.
            return StatusCode(statusCode, new ApiResponse<object>(statusCode: statusCode, isSuccess: false, errorMessage: errorMessage, data: errorDetails));
        }
    }

    /// <summary>
    /// Standard API response wrapper.
    /// </summary>
    /// <typeparam name="T">Type of the response data</typeparam>
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        public ApiResponse(int statusCode, bool isSuccess, T data = default, string errorMessage = null)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
        }
    }
}
