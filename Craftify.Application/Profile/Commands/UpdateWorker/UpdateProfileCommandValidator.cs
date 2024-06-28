using FluentValidation;


namespace Craftify.Application.Profile.Commands.UpdateWorker
{
    public class UpdateWorkerCommandValidator : AbstractValidator<UpdateWorkerCommand>
    {
        public UpdateWorkerCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}
