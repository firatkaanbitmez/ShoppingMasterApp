using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands;
using ShoppingMasterApp.Application.CQRS.Queries.Shipping;
using ShoppingMasterApp.Application.DTOs;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingController : BaseController
    {
        private readonly IMediator _mediator;

        public ShippingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShipping([FromBody] CreateShippingCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("Shipping record created successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShipping([FromBody] UpdateShippingCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("Shipping record updated successfully.");
        }

        [HttpPost("calculate-cost")]
        public async Task<IActionResult> CalculateShippingCost([FromBody] CalculateShippingCostCommand command)
        {
            var cost = await _mediator.Send(command);
            return ApiResponse(new { Cost = cost });
        }

        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetShippingByOrderId(int orderId)
        {
            var shipping = await _mediator.Send(new GetShippingByOrderIdQuery { OrderId = orderId });
            return ApiResponse(shipping);
        }
    }
}
