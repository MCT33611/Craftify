using MediatR;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Reviews.Common;

namespace Craftify.Application.Ratings.Queries.GetAverageRatingForProvider
{
    public class GetAverageRatingForProviderQueryHandler
        : IRequestHandler<GetAverageRatingForProviderQuery, ProviderAverageRatingVm>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAverageRatingForProviderQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProviderAverageRatingVm> Handle(GetAverageRatingForProviderQuery request, CancellationToken cancellationToken)
        {
            var averageOverallRating = await _unitOfWork.ReviewRating.GetAverageRatingForProvider(request.ProviderId);
            var reviewCount = await _unitOfWork.ReviewRating.GetReviewCountForProvider(request.ProviderId);

            // Assuming you want to get average for each category as well
            var reviews = await _unitOfWork.ReviewRating.GetReviewsByProviderId(request.ProviderId);

            double avgPunctuality = 0;
            double avgProfessionalism = 0;
            double avgQuality = 0;

            if (reviewCount > 0)
            {
                avgPunctuality = reviews.Average(r => r.Rating.PunctualityScore);
                avgProfessionalism = reviews.Average(r => r.Rating.ProfessionalismScore);
                avgQuality = reviews.Average(r => r.Rating.QualityScore);
            }

            return new ProviderAverageRatingVm
            {
                ProviderId = request.ProviderId,
                AverageOverallRating = averageOverallRating,
                AveragePunctualityRating = avgPunctuality,
                AverageProfessionalismRating = avgProfessionalism,
                AverageQualityRating = avgQuality,
                TotalReviews = reviewCount
            };
        }
    }
}