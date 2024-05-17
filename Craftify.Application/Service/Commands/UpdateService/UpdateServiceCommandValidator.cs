using FluentValidation;


namespace Craftify.Application.Service.Commands.UpdateService
{
    public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
    {
        public UpdateServiceCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}
