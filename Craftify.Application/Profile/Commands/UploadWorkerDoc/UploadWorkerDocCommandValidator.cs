using Craftify.Application.Profile.Commands.UploadWorkerDoc;
using FluentValidation;


namespace Craftify.Application.Profile.Commands.UploadWorkerDoc
{
    public class UploadWorkerDocCommandValidator : AbstractValidator<UploadWorkerDocCommand>
    {
        public UploadWorkerDocCommandValidator()
        {
            RuleFor(x => x.File).NotEmpty();
        }
    }

}
