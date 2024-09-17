using Microsoft.AspNetCore.Mvc;
using MediatR;
using ShoppingMasterApp.Application.CQRS.Commands.Cart;
using ShoppingMasterApp.Application.CQRS.Queries.Cart;
using System.Threading.Tasks;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Application.Interfaces;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : BaseController
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator, ITokenService tokenService)
     : base(tokenService)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("Product added to cart successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartItemCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("Cart item updated successfully.");
        }

        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId, [FromQuery] int customerId)
        {
            await _mediator.Send(new RemoveFromCartCommand { ProductId = productId, CustomerId = customerId });
            return ApiResponse("Product removed from cart.");
        }

        [HttpDelete("clear/{customerId}")]
        public async Task<IActionResult> ClearCart(int customerId)
        {
            await _mediator.Send(new ClearCartCommand { CustomerId = customerId });
            return ApiResponse("Cart cleared successfully.");
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomerCart(int customerId)
        {
            var cart = await _mediator.Send(new GetCustomerCartQuery { CustomerId = customerId });
            return ApiResponse(cart);
        }
    }
}
