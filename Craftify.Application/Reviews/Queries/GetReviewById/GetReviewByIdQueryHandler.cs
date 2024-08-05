using MediatR;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Common.Errors;
using Craftify.Domain.Entities;
using Craftify.Application.Reviews.Common;

namespace Craftify.Application.Reviews.Queries.GetReviewById
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewDetailVm>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReviewByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ReviewDetailVm> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var review = await _unitOfWork.ReviewRating.GetReviewById(request.Id);

            if (review == null)
            {
                throw new NotFoundException("Review not forunt");
            }

            return new ReviewDetailVm
            {
                Id = review.Id,
                BookingId = review.BookingId,
                CustomerId = review.CustomerId,
                ProviderId = review.ProviderId,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                Rating = new RatingVm
                {
                    OverallScore = review.Rating.OverallScore,
                    PunctualityScore = review.Rating.PunctualityScore,
                    ProfessionalismScore = review.Rating.ProfessionalismScore,
                    QualityScore = review.Rating.QualityScore
                }
            };
        }
    }
}