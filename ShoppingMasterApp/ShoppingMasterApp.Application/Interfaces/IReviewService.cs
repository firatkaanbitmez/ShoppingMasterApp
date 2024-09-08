using ShoppingMasterApp.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task AddReviewAsync(ReviewDto reviewDto);
        Task UpdateReviewAsync(ReviewDto reviewDto);
        Task DeleteReviewAsync(int reviewId);
        Task<ReviewDto> GetReviewByProductIdAsync(int productId);
        Task<IEnumerable<ReviewDto>> GetUserReviewsAsync(int userId);
    }
}
