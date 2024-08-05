using MediatR;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Reviews.Common;

namespace Craftify.Application.Reviews.Queries.GetReviewsByBookingId
{
    public class GetReviewsByBookingIdQueryHandler : IRequestHandler<GetReviewsByBookingIdQuery, List<ReviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReviewsByBookingIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ReviewDto>> Handle(GetReviewsByBookingIdQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _unitOfWork.ReviewRating.GetReviewsByBookingId(request.BookingId);

            return reviews.Select(review => new ReviewDto
            {
                Id = review.Id,
                CustomerId = review.CustomerId,
                ProviderId = review.ProviderId,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                Rating = new RatingDto
                {
                    OverallScore  =  review.Rating.OverallScore,
                    PunctualityScore = review.Rating.PunctualityScore,
                    ProfessionalismScore = review.Rating.ProfessionalismScore,
                    QualityScore = review.Rating.QualityScore
                }
            }).ToList();
        }
    }
}