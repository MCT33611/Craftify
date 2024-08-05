using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Reviews.Common;

namespace Craftify.Application.Reviews.Queries.GetAllReviewsWithRatings
{
    public class GetAllReviewsWithRatingsQueryHandler
        : IRequestHandler<GetAllReviewsWithRatingsQuery, ReviewsWithRatingsVm>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllReviewsWithRatingsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ReviewsWithRatingsVm> Handle(GetAllReviewsWithRatingsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _unitOfWork.ReviewRating.GetAllReviewsWithRatings();

            var totalReviews = reviews.Count();
            var totalPages = (totalReviews + request.PageSize - 1) / request.PageSize;

            var paginatedReviews = reviews
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(review => new ReviewWithRatingDto
                {
                    Id = review.Id,
                    BookingId = review.BookingId,
                    CustomerId = review.CustomerId,
                    ProviderId = review.ProviderId,
                    Comment = review.Comment,
                    CreatedAt = review.CreatedAt,
                    Rating = new RatingDto
                    {
                        OverallScore = review.Rating.OverallScore,
                        PunctualityScore = review.Rating.PunctualityScore,
                        ProfessionalismScore = review.Rating.ProfessionalismScore,
                        QualityScore = review.Rating.QualityScore
                    }
                })
                .ToList();

            return new ReviewsWithRatingsVm
            {
                Reviews = paginatedReviews,
                CurrentPage = request.Page,
                PageSize = request.PageSize,
                TotalPages = totalPages,
                TotalReviews = totalReviews
            };
        }
    }
}