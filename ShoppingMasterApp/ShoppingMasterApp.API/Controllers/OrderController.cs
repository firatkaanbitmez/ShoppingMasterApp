using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Order;
using ShoppingMasterApp.Application.CQRS.Queries.Order;

namespace ShoppingMasterApp.API.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderCommand command)
        {
            if (id != command.Id) return ApiError("Invalid Order ID.");
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _mediator.Send(new DeleteOrderCommand { Id = id });
            return ApiResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery { Id = id });
            return ApiResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery());
            return ApiResponse(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserOrders(int userId)
        {
            var result = await _mediator.Send(new GetUserOrdersQuery { UserId = userId });
            return ApiResponse(result);
        }
    }
}
