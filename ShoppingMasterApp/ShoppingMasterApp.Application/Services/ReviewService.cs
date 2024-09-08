using ShoppingMasterApp.Application.CQRS.Commands.Review;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using ShoppingMasterApp.Application.DTOs;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task AddReviewAsync(ReviewDto reviewDto)
    {
        var review = new Review
        {
            ProductId = reviewDto.ProductId,
            UserId = reviewDto.UserId,
            Rating = reviewDto.Rating,
            Comment = reviewDto.Comment,
            CreatedAt = DateTime.Now
        };

        await _reviewRepository.AddAsync(review);
    }

    public async Task UpdateReviewAsync(ReviewDto reviewDto)
    {
        var review = await _reviewRepository.GetByIdAsync(reviewDto.Id);
        if (review == null)
        {
            throw new Exception("Review not found");
        }

        review.Rating = reviewDto.Rating;
        review.Comment = reviewDto.Comment;

        await _reviewRepository.UpdateAsync(review);
    }

    public async Task DeleteReviewAsync(int reviewId)
    {
        var review = await _reviewRepository.GetByIdAsync(reviewId);
        if (review == null)
        {
            throw new Exception("Review not found");
        }

        await _reviewRepository.DeleteAsync(review);
    }

    public async Task<ReviewDto> GetReviewByProductIdAsync(int productId)
    {
        var review = (await _reviewRepository.GetByProductIdAsync(productId)).FirstOrDefault();
        if (review == null)
        {
            throw new Exception("Review not found for this product");
        }

        return new ReviewDto
        {
            ProductId = review.ProductId,
            UserId = review.UserId,
            Rating = review.Rating,
            Comment = review.Comment
        };
    }


    public async Task<IEnumerable<ReviewDto>> GetUserReviewsAsync(int userId)
    {
        var reviews = await _reviewRepository.GetByUserIdAsync(userId);
        if (reviews == null || !reviews.Any())
        {
            throw new Exception("No reviews found for this user");
        }

        return reviews.Select(review => new ReviewDto
        {
            ProductId = review.ProductId,
            UserId = review.UserId,
            Rating = review.Rating,
            Comment = review.Comment
        }).ToList();
    }

}
