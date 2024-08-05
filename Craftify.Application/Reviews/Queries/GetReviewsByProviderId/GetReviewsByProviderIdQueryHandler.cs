using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Reviews.Common;

namespace Craftify.Application.Reviews.Queries.GetReviewsByProviderId
{
    public class GetReviewsByProviderIdQueryHandler : IRequestHandler<GetReviewsByProviderIdQuery, ProviderReviewsVm>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReviewsByProviderIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProviderReviewsVm> Handle(GetReviewsByProviderIdQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _unitOfWork.ReviewRating.GetReviewsByProviderId(request.ProviderId);

            var reviewDtos = reviews.Select(review => new ProviderReviewDto
            {
                Id = review.Id,
                BookingId = review.BookingId,
                CustomerId = review.CustomerId,
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

            var averageRating = await _unitOfWork.ReviewRating.GetAverageRatingForProvider(request.ProviderId);

            return new ProviderReviewsVm
            {
                ProviderId = request.ProviderId,
                Reviews = reviewDtos,
                AverageRating = averageRating,
                TotalReviews = reviewDtos.Count
            };
        }
    }
}