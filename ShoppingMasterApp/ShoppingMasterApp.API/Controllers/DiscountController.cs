using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Discount;
using ShoppingMasterApp.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountController : BaseController
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyDiscount([FromBody] ApplyDiscountCommand command)
        {
            await _discountService.ApplyDiscountAsync(command);
            return ApiResponse("Discount applied successfully");
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountCommand command)
        {
            await _discountService.CreateDiscountAsync(command);
            return ApiResponse("Discount created successfully");
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveDiscount(int id)
        {
            await _discountService.RemoveDiscountAsync(new RemoveDiscountCommand { Id = id });
            return ApiResponse("Discount removed successfully");
        }
    }
}
