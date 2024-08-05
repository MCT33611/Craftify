using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Craftify.Infrastructure.Presistence.Repository
{
    public class ReviewRatingRepository : IReviewRatingRepository
    {
        private readonly CraftifyDbContext _context;

        public ReviewRatingRepository(CraftifyDbContext context)
        {
            _context = context;
        }

        public async Task<Review> AddReview(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<Review> GetReviewById(Guid id)
        {
            return await _context.Reviews
                .Include(r => r.Rating)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Review>> GetReviewsByBookingId(Guid bookingId)
        {
            return await _context.Reviews
                .Include(r => r.Rating)
                .Where(r => r.BookingId == bookingId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByCustomerId(Guid customerId)
        {
            return await _context.Reviews
                .Include(r => r.Rating)
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByProviderId(Guid providerId)
        {
            return await _context.Reviews
                .Include(r => r.Rating)
                .Where(r => r.ProviderId == providerId)
                .ToListAsync();
        }

        public async Task<Rating> AddRating(Rating rating)
        {
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<Rating> UpdateRating(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<double> GetAverageRatingForProvider(Guid providerId)
        {
            return await _context.Ratings
                .Where(r => r.Review.ProviderId == providerId)
                .AverageAsync(r => r.OverallScore);
        }

        public async Task<IEnumerable<Review>> GetAllReviewsWithRatings()
        {
            return await _context.Reviews
                .Include(r => r.Rating)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetRecentReviews(int count)
        {
            return await _context.Reviews
                .Include(r => r.Rating)
                .OrderByDescending(r => r.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Worker>> GetTopRatedProviders(int count)
        {
            return await _context.Workers
                .Where(w => w.Id == w.Id) 
                .Select(w => new
                {
                    Worker = w,
                    AverageRating = w.Id == w.Id ? _context.Reviews
                        .Where(r => r.ProviderId == w.Id)
                        .Select(r => r.Rating.OverallScore)
                        .DefaultIfEmpty()
                        .Average() : 0
                })
                .Include("User")
                .OrderByDescending(x => x.AverageRating)
                .Take(count)
                .Select(x => x.Worker)
                .ToListAsync();
        }

        public async Task<int> GetReviewCountForProvider(Guid providerId)
        {
            return await _context.Reviews
                .CountAsync(r => r.ProviderId == providerId);
        }

        public async Task<IEnumerable<Review>> SearchReviewsByKeyword(string keyword)
        {
            return await _context.Reviews
                .Include(r => r.Rating)
                .Where(r => r.Comment.Contains(keyword))
                .ToListAsync();
        }
    }
}
