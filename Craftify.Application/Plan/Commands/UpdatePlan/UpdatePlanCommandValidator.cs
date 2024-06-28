using FluentValidation;


namespace Craftify.Application.Plan.Commands.UpdatePlan
{
    public class UpdatePlanCommandValidator : AbstractValidator<UpdatePlanCommand>
    {
        public UpdatePlanCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}
