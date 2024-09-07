using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Discount;

namespace ShoppingMasterApp.API.Controllers
{
    public class DiscountController : BaseController
    {
        private readonly IMediator _mediator;

        public DiscountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> ApplyDiscount(ApplyDiscountCommand command)
        {
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveDiscount(int id)
        {
            var result = await _mediator.Send(new RemoveDiscountCommand { Id = id });
            return ApiResponse(result);
        }
    }
}
