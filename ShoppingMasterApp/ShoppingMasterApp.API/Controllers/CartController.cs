using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Cart;
using ShoppingMasterApp.Application.Interfaces.Services;

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
            return ApiResponse(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
        {
            await _cartService.AddToCartAsync(command);
            return ApiResponse(message: "Product added to cart successfully");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartItemCommand command)
        {
            await _cartService.UpdateCartItemAsync(command);
            return ApiResponse(message: "Cart updated successfully");
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartCommand command)
        {
            await _cartService.RemoveFromCartAsync(command);
            return ApiResponse(message: "Item removed from cart successfully");
        }

        [HttpDelete("clear/{cartId}")]
        public async Task<IActionResult> ClearCart(int cartId)
        {
            await _cartService.ClearCartAsync(cartId);
            return ApiResponse(message: "Cart cleared successfully");
        }
    }
}
