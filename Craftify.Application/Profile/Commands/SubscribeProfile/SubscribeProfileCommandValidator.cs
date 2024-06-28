using Craftify.Application.Profile.Commands.UpdateProfile;
using FluentValidation;


namespace Craftify.Application.Profile.Commands.SubscribeProfile
{
    public class SubscribeProfileCommandValidator : AbstractValidator<SubscribeProfileCommand>
    {
        public SubscribeProfileCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.PlanId).NotEmpty();
            RuleFor(x => x.PaymentId).NotEmpty();
            RuleFor(x => x.ServiceTitle).NotEmpty();
        }
    }

}
