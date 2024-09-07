using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Cart;
using ShoppingMasterApp.Application.CQRS.Queries.Cart;

namespace ShoppingMasterApp.API.Controllers
{
    public class CartController : BaseController
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartCommand command)
        {
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem(UpdateCartItemCommand command)
        {
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromCart(RemoveFromCartCommand command)
        {
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart(ClearCartCommand command)
        {
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserCart(int userId)
        {
            var result = await _mediator.Send(new GetUserCartQuery { UserId = userId });
            return ApiResponse(result);
        }
    }
}
