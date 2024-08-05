using Craftify.Application.Reviews.Commands.CreateRating;
using FluentValidation;

namespace Craftify.Application.Ratings.Commands.CreateRating
{
    public class CreateRatingCommandValidator : AbstractValidator<CreateRatingCommand>
    {
        public CreateRatingCommandValidator()
        {
            RuleFor(x => x.ReviewId).NotEmpty();
            RuleFor(x => x.OverallScore).InclusiveBetween(1, 5);
            RuleFor(x => x.PunctualityScore).InclusiveBetween(1, 5);
            RuleFor(x => x.ProfessionalismScore).InclusiveBetween(1, 5);
            RuleFor(x => x.QualityScore).InclusiveBetween(1, 5);
        }
    }
}