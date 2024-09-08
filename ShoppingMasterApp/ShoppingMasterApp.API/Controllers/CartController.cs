using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Cart;
using ShoppingMasterApp.Application.CQRS.Queries.Cart;
using ShoppingMasterApp.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
        {
            await _cartService.AddToCartAsync(command);
            return ApiResponse("Product added to cart successfully");
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart([FromBody] RemoveFromCartCommand command)
        {
            await _cartService.RemoveFromCartAsync(command);
            return ApiResponse("Product removed from cart successfully");
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart([FromBody] ClearCartCommand command)
        {
            await _cartService.ClearCartAsync(command);
            return ApiResponse("Cart cleared successfully");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartByUserId(int userId)
        {
            var result = await _cartService.GetCartByUserIdAsync(userId);
            return ApiResponse(result);
        }
    }
}
