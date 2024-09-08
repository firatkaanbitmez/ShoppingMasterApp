using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Review;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : BaseController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddReview([FromBody] AddReviewCommand command)
        {
            var reviewDto = new ReviewDto
            {
                ProductId = command.ProductId,
                UserId = command.UserId,
                Comment = command.Comment,
                Rating = command.Rating
            };

            await _reviewService.AddReviewAsync(reviewDto);
            return ApiResponse("Review added successfully");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewCommand command)
        {
            var reviewDto = new ReviewDto
            {
                Id = command.Id,
                Comment = command.Comment,
                Rating = command.Rating
            };

            await _reviewService.UpdateReviewAsync(reviewDto);
            return ApiResponse("Review updated successfully");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            await _reviewService.DeleteReviewAsync(id);
            return ApiResponse("Review deleted successfully");
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetReviewsByProductId(int productId)
        {
            var result = await _reviewService.GetReviewByProductIdAsync(productId);
            return ApiResponse(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetReviewsByUserId(int userId)
        {
            var result = await _reviewService.GetUserReviewsAsync(userId);
            return ApiResponse(result);
        }
    }
}
