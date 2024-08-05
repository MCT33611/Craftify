using FluentValidation;

namespace Craftify.Application.Reviews.Queries.GetRecentReviews
{
    public class GetRecentReviewsQueryValidator : AbstractValidator<GetRecentReviewsQuery>
    {
        public GetRecentReviewsQueryValidator()
        {
            RuleFor(v => v.Count).GreaterThan(0).LessThanOrEqualTo(50);
        }
    }
}