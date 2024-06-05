using Craftify.Application.Profile.Queries.GetProfile;
using FluentValidation;

namespace Craftify.Application.Service.Queries.GetAllService
{
    public class GetAllProfilesQueryValidator : AbstractValidator<GetProfileQuery>
    {
        public GetAllProfilesQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

    }
}
