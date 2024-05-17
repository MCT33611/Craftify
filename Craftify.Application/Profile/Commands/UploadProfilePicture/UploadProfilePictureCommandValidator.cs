using Craftify.Application.Profile.Commands.UploadProfilePicture;
using FluentValidation;


namespace Craftify.Application.Profile.Commands.UpdateProfile
{
    public class UploadProfilePictureCommandValidator : AbstractValidator<UploadProfilePictureCommand>
    {
        public UploadProfilePictureCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.File).NotEmpty();
        }
    }

}
