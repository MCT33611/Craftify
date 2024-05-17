using Craftify.Application.Service.Commands.UpdateService;
using FluentValidation;


namespace Craftify.Application.Profile.Commands.UpdateService
{
    public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
    {
        public UpdateServiceCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.Availability).NotEmpty();
            RuleFor(x => x.ZipCode).NotEmpty();
        }
    }

}
