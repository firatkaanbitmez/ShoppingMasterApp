using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Review;
using ShoppingMasterApp.Application.CQRS.Queries.Review;

namespace ShoppingMasterApp.API.Controllers
{
    public class ReviewController : BaseController
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewCommand command)
        {
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, UpdateReviewCommand command)
        {
            if (id != command.Id) return ApiError("Invalid Review ID.");
            var result = await _mediator.Send(command);
            return ApiResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _mediator.Send(new DeleteReviewCommand { Id = id });
            return ApiResponse(result);
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetReviewsByProductId(int productId)
        {
            var result = await _mediator.Send(new GetReviewByProductIdQuery { ProductId = productId });
            return ApiResponse(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserReviews(int userId)
        {
            var result = await _mediator.Send(new GetUserReviewsQuery { UserId = userId });
            return ApiResponse(result);
        }
    }
}
