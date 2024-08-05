using FluentValidation;

namespace Craftify.Application.Reviews.Queries.GetReviewsByCustomerId
{
    public class GetReviewsByCustomerIdQueryValidator : AbstractValidator<GetReviewsByCustomerIdQuery>
    {
        public GetReviewsByCustomerIdQueryValidator()
        {
            RuleFor(v => v.CustomerId).NotEmpty();
        }
    }
}