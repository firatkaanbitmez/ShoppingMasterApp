using ShoppingMasterApp.Application.CQRS.Commands.Review;
using ShoppingMasterApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task AddReviewAsync(AddReviewCommand command);
        Task UpdateReviewAsync(UpdateReviewCommand command);
        Task DeleteReviewAsync(int reviewId);
        Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId);
        Task<IEnumerable<Review>> GetUserReviewsAsync(int userId);
    }


}
