using FluentValidation;


namespace Craftify.Application.Profile.Commands.UpdateProfile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}
