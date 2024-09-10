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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartByUserId(int userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return ApiResponse(cart);
        }

        [HttpPost("addOrUpdate")]
        public async Task<IActionResult> AddOrUpdateCartItem([FromBody] AddToCartCommand command)
        {
            await _cartService.AddOrUpdateCartItemAsync(command);
            return ApiResponse("Product added or updated in the cart successfully.");
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveCartItem([FromBody] RemoveFromCartCommand command)
        {
            await _cartService.RemoveCartItemAsync(command);
            return ApiResponse("Product removed from the cart successfully.");
        }

        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _cartService.ClearCartAsync(userId);
            return ApiResponse("Cart cleared successfully.");
        }
    }






}
