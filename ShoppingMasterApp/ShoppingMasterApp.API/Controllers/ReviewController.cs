using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Review;
using ShoppingMasterApp.Application.Interfaces.Services;

namespace ShoppingMasterApp.API.Controllers
{
    public class ReviewController : BaseController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] AddReviewCommand command)
        {
            await _reviewService.AddReviewAsync(command);
            return ApiResponse(message: "Review added successfully");
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            await _reviewService.DeleteReviewAsync(reviewId);
            return ApiResponse(message: "Review deleted successfully");
        }
    }
}
