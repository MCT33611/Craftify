using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Domain.Entities;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Reviews.Commands.CreateRating;

namespace Craftify.Application.Ratings.Commands.CreateRating
{
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, CreateRatingResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRatingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateRatingResult> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            var review = await _unitOfWork.ReviewRating.GetReviewById(request.ReviewId);
            if (review == null)
            {
                throw new ApplicationException($"Review with ID {request.ReviewId} not found.");
            }

            var rating = new Rating
            {
                ReviewId = request.ReviewId,
                OverallScore = request.OverallScore,
                PunctualityScore = request.PunctualityScore,
                ProfessionalismScore = request.ProfessionalismScore,
                QualityScore = request.QualityScore
            };

            await _unitOfWork.ReviewRating.AddRating(rating);
            await _unitOfWork.Save();

            return new CreateRatingResult
            {
                RatingId = rating.Id,
                ReviewId = rating.ReviewId
            };
        }
    }
}