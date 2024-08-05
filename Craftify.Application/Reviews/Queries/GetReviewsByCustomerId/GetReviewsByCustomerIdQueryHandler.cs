using MediatR;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Reviews.Common;

namespace Craftify.Application.Reviews.Queries.GetReviewsByCustomerId
{
    public class GetReviewsByCustomerIdQueryHandler : IRequestHandler<GetReviewsByCustomerIdQuery, List<CustomerReviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReviewsByCustomerIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CustomerReviewDto>> Handle(GetReviewsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _unitOfWork.ReviewRating.GetReviewsByCustomerId(request.CustomerId);

            return reviews.Select(review => new CustomerReviewDto
            {
                Id = review.Id,
                BookingId = review.BookingId,
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