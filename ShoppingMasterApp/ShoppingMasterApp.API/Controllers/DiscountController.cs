using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Discount;
using ShoppingMasterApp.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
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
            return ApiSuccess<object>(null, "Discount applied successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDiscount(int id)
        {
            await _discountService.RemoveDiscountAsync(id);
            return ApiSuccess<object>(null, "Discount removed successfully");
        }
    }
}
