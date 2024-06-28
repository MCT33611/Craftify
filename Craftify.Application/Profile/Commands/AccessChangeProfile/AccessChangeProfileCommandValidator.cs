using Craftify.Application.Profile.Commands.AccessChangeProfile;
using FluentValidation;


namespace Craftify.Application.Profile.Commands.UpdateProfile
{
    public class AccessChangeProfileCommandValidator : AbstractValidator<AccessChangeProfileCommand>
    {
        public AccessChangeProfileCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}
