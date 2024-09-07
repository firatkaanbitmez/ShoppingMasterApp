using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Order;
using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return ApiSuccess(orders, "Orders retrieved successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return ApiNotFound("Order not found");
            }

            return ApiSuccess(order, "Order retrieved successfully");
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            await _orderService.CreateOrderAsync(command);
            return ApiSuccess<object>(null, "Order created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderCommand command)
        {
            command.Id = id;
            await _orderService.UpdateOrderAsync(command);
            return ApiSuccess<object>(null, "Order updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return ApiSuccess<object>(null, "Order deleted successfully");
        }
    }
}
