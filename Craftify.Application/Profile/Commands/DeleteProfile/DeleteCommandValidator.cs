
using Craftify.Application.Profile.Commands.DeleteProfile;
using FluentValidation;


namespace Craftify.Application.Profile.Commands.DeleteProfile
{
    public class DeleteProfileCommandValidator : AbstractValidator<DeleteProfileCommand>
    {
        public DeleteProfileCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}
