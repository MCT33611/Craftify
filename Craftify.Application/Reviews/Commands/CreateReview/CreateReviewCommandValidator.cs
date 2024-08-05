using FluentValidation;

namespace Craftify.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.ProviderId).NotEmpty();
            RuleFor(x => x.Comment).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.OverallScore).InclusiveBetween(1, 5);
            RuleFor(x => x.PunctualityScore).InclusiveBetween(1, 5);
            RuleFor(x => x.ProfessionalismScore).InclusiveBetween(1, 5);
            RuleFor(x => x.QualityScore).InclusiveBetween(1, 5);
        }
    }
}