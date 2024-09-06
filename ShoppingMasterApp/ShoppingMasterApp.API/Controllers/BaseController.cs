using Microsoft.AspNetCore.Mvc;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HandleResponse<T>(T result)
        {
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        protected IActionResult HandleErrorResponse(string errorMessage)
        {
            return BadRequest(new { Error = errorMessage });
        }
    }
}
