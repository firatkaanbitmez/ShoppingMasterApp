using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult ApiResponse<T>(T result, string message = null, int statusCode = 200)
        {
            return StatusCode(statusCode, new ApiResponse<T>
            {
                StatusCode = statusCode,
                IsSuccess = true,
                Data = result,
                ErrorMessage = null,
                ResponseStatus = ResponseStatus.Success.ToString(),
                Message = message
            });
        }

        protected IActionResult ApiError(string message, int statusCode = 400, ResponseStatus responseStatus = ResponseStatus.Error)
        {
            return StatusCode(statusCode, new ApiResponse<object>
            {
                StatusCode = statusCode,
                IsSuccess = false,
                ErrorMessage = message,
                ResponseStatus = responseStatus.ToString(),
                Data = null
            });
        }

        protected IActionResult ApiValidationError(IEnumerable<string> errors)
        {
            return BadRequest(new ApiResponse<object>
            {
                StatusCode = 400,
                IsSuccess = false,
                ErrorMessage = "Validation error",
                ResponseStatus = ResponseStatus.ValidationError.ToString(),
                Data = new { Errors = errors }
            });
        }

        // Extract user ID from token
        protected int GetUserIdFromToken()
        {
            if (User.Identity is ClaimsIdentity identity)
            {
                var userIdClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
            }
            throw new UnauthorizedAccessException("User ID could not be extracted from the token.");
        }
    }

    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public string ResponseStatus { get; set; }
        public string Message { get; set; }
    }

    public enum ResponseStatus
    {
        Success,
        Error,
        ValidationError,
        NotFound,
        Unauthorized,
        ServerError
    }
}
