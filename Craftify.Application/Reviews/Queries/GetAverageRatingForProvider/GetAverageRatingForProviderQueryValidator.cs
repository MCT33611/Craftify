using FluentValidation;

namespace Craftify.Application.Ratings.Queries.GetAverageRatingForProvider
{
    public class GetAverageRatingForProviderQueryValidator
        : AbstractValidator<GetAverageRatingForProviderQuery>
    {
        public GetAverageRatingForProviderQueryValidator()
        {
            RuleFor(v => v.ProviderId).NotEmpty();
        }
    }
}