using FluentValidation;


namespace Craftify.Application.Profile.Commands.UpdateServiceProvider
{
    public class UpdateServiceProviderCommandValidator : AbstractValidator<UpdateServiceProviderCommand>
    {
        public UpdateServiceProviderCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}
