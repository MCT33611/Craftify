using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Domain.Entities;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Reviews.Common;

namespace Craftify.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, CreateReviewResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateReviewCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateReviewResult> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = new Review
            {
                BookingId = request.BookingId,
                CustomerId = request.CustomerId,
                ProviderId = request.ProviderId,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow,
                Rating = new Rating
                {
                    OverallScore = request.OverallScore,
                    PunctualityScore = request.PunctualityScore,
                    ProfessionalismScore = request.ProfessionalismScore,
                    QualityScore = request.QualityScore
                }
            };

            await _unitOfWork.ReviewRating.AddReview(review);
            await _unitOfWork.Save();

            return new CreateReviewResult
            {
                ReviewId = review.Id,
                CreatedAt = review.CreatedAt
            };
        }
    }
}