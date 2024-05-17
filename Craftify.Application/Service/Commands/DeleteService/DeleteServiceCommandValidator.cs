using Craftify.Application.Service.Commands.CreateService;
using FluentValidation;


namespace Craftify.Application.Service.Commands.DeleteService
{
    public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
    {
        public DeleteServiceCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}
