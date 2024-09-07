using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Shipping;
using ShoppingMasterApp.Application.CQRS.Queries.Shipping;

namespace ShoppingMasterApp.API.Controllers
{
    public class ShippingController : BaseController
    {
        private readonly IMediator _mediator;

        public ShippingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShipping(CreateShippingCommand command)
        {
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipping(int id, UpdateShippingCommand command)
        {
            if (id != command.Id) return ApiError("Invalid Shipping ID.");
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetShippingByOrderId(int orderId)
        {
            var result = await _mediator.Send(new GetShippingByOrderIdQuery { OrderId = orderId });
            return ApiResponse(result);
        }
    }
}
