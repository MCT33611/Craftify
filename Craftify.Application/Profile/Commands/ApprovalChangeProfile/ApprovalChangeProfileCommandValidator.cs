using Craftify.Application.Profile.Commands.AccessChangeProfile;
using Craftify.Application.Profile.Commands.ApprovalChangeProfile;
using FluentValidation;


namespace Craftify.Application.Profile.Commands.UpdateProfile
{
    public class ApprovalChangeProfileCommandValidator : AbstractValidator<ApprovalChangeProfileCommand>
    {
        public ApprovalChangeProfileCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}
