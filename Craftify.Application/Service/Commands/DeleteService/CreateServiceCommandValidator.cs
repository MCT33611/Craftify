using Craftify.Application.Service.Commands.CreateService;
using Craftify.Application.Service.Commands.DeleteService;
using FluentValidation;


namespace Craftify.Application.Profile.Commands.CreateService
{
    public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
    {
        public DeleteServiceCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}
