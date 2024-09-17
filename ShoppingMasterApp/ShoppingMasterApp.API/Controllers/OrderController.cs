using Microsoft.AspNetCore.Mvc;
using MediatR;
using ShoppingMasterApp.Application.CQRS.Commands.Order;
using ShoppingMasterApp.Application.CQRS.Queries.Order;
using System.Threading.Tasks;
using ShoppingMasterApp.Application.Interfaces;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;


        public OrderController(IMediator mediator, ITokenService tokenService)
     : base(tokenService)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return ApiResponse(orderId, "Order created successfully.");
        }

        [HttpPut("cancel/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var result = await _mediator.Send(new CancelOrderCommand { OrderId = orderId });
            return ApiResponse(result ? "Order cancelled successfully." : "Order cancellation failed.");
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery { OrderId = orderId });
            return ApiResponse(order);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomerId(int customerId)
        {
            var orders = await _mediator.Send(new GetOrdersByCustomerIdQuery { CustomerId = customerId });
            return ApiResponse(orders);
        }
    }
}
