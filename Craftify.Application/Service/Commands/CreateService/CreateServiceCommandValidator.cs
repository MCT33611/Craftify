using Craftify.Application.Service.Commands.CreateService;
using FluentValidation;


namespace Craftify.Application.Profile.Commands.CreateService
{
    public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.Availability).NotEmpty();
            RuleFor(x => x.ZipCode).NotEmpty();
        }
    }

}
