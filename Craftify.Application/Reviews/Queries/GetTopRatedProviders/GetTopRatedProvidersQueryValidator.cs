using FluentValidation;

namespace Craftify.Application.Providers.Queries.GetTopRatedProviders
{
    public class GetTopRatedProvidersQueryValidator : AbstractValidator<GetTopRatedProvidersQuery>
    {
        public GetTopRatedProvidersQueryValidator()
        {
            RuleFor(v => v.Count).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}