using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Cart;
using ShoppingMasterApp.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _cartService.GetUserCartAsync(userId);
            return ApiSuccess(cart, "Cart retrieved successfully");
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
        {
            await _cartService.AddToCartAsync(command);
            return ApiSuccess<object>(null, "Product added to cart successfully");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartItemCommand command)
        {
            await _cartService.UpdateCartItemAsync(command);
            return ApiSuccess<object>(null, "Cart updated successfully");
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartCommand command)
        {
            await _cartService.RemoveFromCartAsync(command);
            return ApiSuccess<object>(null, "Item removed from cart successfully");
        }

        [HttpDelete("clear/{cartId}")]
        public async Task<IActionResult> ClearCart(int cartId)
        {
            await _cartService.ClearCartAsync(cartId);
            return ApiSuccess<object>(null, "Cart cleared successfully");
        }
    }
}
