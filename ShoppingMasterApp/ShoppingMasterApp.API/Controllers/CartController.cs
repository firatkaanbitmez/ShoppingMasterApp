using Microsoft.AspNetCore.Mvc;
using MediatR;
using ShoppingMasterApp.Application.CQRS.Commands.Cart;
using ShoppingMasterApp.Application.CQRS.Queries.Cart;
using System.Threading.Tasks;
using ShoppingMasterApp.Domain.Entities;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : BaseController
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("Product added to cart successfully.");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartItemCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("Cart item updated successfully.");
        }

        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId, [FromQuery] int userId)
        {
            await _mediator.Send(new RemoveFromCartCommand { ProductId = productId, UserId = userId });
            return ApiResponse("Product removed from cart.");
        }

        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _mediator.Send(new ClearCartCommand { UserId = userId });
            return ApiResponse("Cart cleared successfully.");
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserCart(int userId)
        {
            var cart = await _mediator.Send(new GetUserCartQuery { UserId = userId });
            return ApiResponse(cart);
        }
    }
}
