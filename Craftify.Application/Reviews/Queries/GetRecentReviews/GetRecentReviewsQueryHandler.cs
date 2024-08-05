using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Reviews.Common;

namespace Craftify.Application.Reviews.Queries.GetRecentReviews
{
    public class GetRecentReviewsQueryHandler
        : IRequestHandler<GetRecentReviewsQuery, List<RecentReviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRecentReviewsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RecentReviewDto>> Handle(GetRecentReviewsQuery request, CancellationToken cancellationToken)
        {
            var recentReviews = await _unitOfWork.ReviewRating.GetRecentReviews(request.Count);

            return recentReviews.Select(review => new RecentReviewDto
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
            }).ToList();
        }
    }
}