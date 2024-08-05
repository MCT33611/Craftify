using FluentValidation;

namespace Craftify.Application.Reviews.Queries.GetAllReviewsWithRatings
{
    public class GetAllReviewsWithRatingsQueryValidator
        : AbstractValidator<GetAllReviewsWithRatingsQuery>
    {
        public GetAllReviewsWithRatingsQueryValidator()
        {
            RuleFor(v => v.Page).GreaterThan(0);
            RuleFor(v => v.PageSize).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}