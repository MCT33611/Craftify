using Craftify.Application.Profile.Commands.UpdateProfile;
using FluentValidation;


namespace Craftify.Application.Profile.Commands.InitSubscribeProfile
{
    public class InitSubscribeProfileCommandValidator : AbstractValidator<InitSubscribeProfileCommand>
    {
        public InitSubscribeProfileCommandValidator()
        {
            RuleFor(x => x.WorkerId).NotEmpty();
            RuleFor(x => x.PlanId).NotEmpty();
        }
    }

}
