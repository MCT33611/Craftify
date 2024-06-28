using FluentValidation;


namespace Craftify.Application.Plan.Commands.DeletePlan
{
    public class DeletePlanCommandValidator : AbstractValidator<DeletePlanCommand>
    {
        public DeletePlanCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}
