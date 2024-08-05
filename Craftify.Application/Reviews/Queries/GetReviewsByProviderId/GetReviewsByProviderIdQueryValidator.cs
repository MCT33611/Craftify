using FluentValidation;

namespace Craftify.Application.Reviews.Queries.GetReviewsByProviderId
{
    public class GetReviewsByProviderIdQueryValidator : AbstractValidator<GetReviewsByProviderIdQuery>
    {
        public GetReviewsByProviderIdQueryValidator()
        {
            RuleFor(v => v.ProviderId).NotEmpty();
        }
    }
}