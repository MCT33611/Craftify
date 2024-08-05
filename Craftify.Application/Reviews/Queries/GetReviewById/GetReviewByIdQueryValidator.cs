using FluentValidation;

namespace Craftify.Application.Reviews.Queries.GetReviewById
{
    public class GetReviewByIdQueryValidator : AbstractValidator<GetReviewByIdQuery>
    {
        public GetReviewByIdQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}