using Craftify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Common.Interfaces.Persistence.IRepository
{
    public interface IReviewRatingRepository
    {
        Task<Review> AddReview(Review review);
        Task<Review> GetReviewById(Guid id);
        Task<IEnumerable<Review>> GetReviewsByBookingId(Guid bookingId);
        Task<IEnumerable<Review>> GetReviewsByCustomerId(Guid customerId);
        Task<IEnumerable<Review>> GetReviewsByProviderId(Guid providerId);
        Task<Rating> AddRating(Rating rating);
        Task<Rating> UpdateRating(Rating rating);
        Task<double> GetAverageRatingForProvider(Guid providerId);
        Task<IEnumerable<Review>> GetAllReviewsWithRatings();
        Task<IEnumerable<Review>> GetRecentReviews(int count);
        Task<IEnumerable<Worker>> GetTopRatedProviders(int count);
        Task<int> GetReviewCountForProvider(Guid providerId);
        Task<IEnumerable<Review>> SearchReviewsByKeyword(string keyword);
    }
}
