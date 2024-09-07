using ShoppingMasterApp.Application.CQRS.Commands.Review;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task AddReviewAsync(AddReviewCommand command)
    {
        if (command.Rating < 1 || command.Rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.");

        var review = new Review
        {
            Comment = command.Comment,
            Rating = command.Rating,
            ProductId = command.ProductId,
            UserId = command.UserId
        };

        await _reviewRepository.AddAsync(review);
        await _reviewRepository.SaveChangesAsync();
    }


    public async Task UpdateReviewAsync(UpdateReviewCommand command)
    {
        var existingReview = await _reviewRepository.GetByIdAsync(command.Id);
        if (existingReview != null)
        {
            existingReview.Comment = command.Comment;
            existingReview.Rating = command.Rating;
            _reviewRepository.Update(existingReview);
            await _reviewRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteReviewAsync(int reviewId)
    {
        var review = await _reviewRepository.GetByIdAsync(reviewId);
        if (review != null)
        {
            _reviewRepository.Delete(review);
            await _reviewRepository.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId)
    {
        return await _reviewRepository.GetReviewsByProductIdAsync(productId);
    }

    public async Task<IEnumerable<Review>> GetUserReviewsAsync(int userId)
    {
        return await _reviewRepository.GetUserReviewsAsync(userId);
    }
}
