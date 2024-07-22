using FluentValidation;

namespace Craftify.Application.Chat.Commands.UnblockUser
{
    public class UnblockUserCommandValidator : AbstractValidator<UnblockUserCommand>
    {
        public UnblockUserCommandValidator()
        {
            RuleFor(x => x.UnblockerId).NotEmpty();
            RuleFor(x => x.UnblockedId).NotEmpty();
            RuleFor(x => x.UnblockerId).NotEqual(x => x.UnblockedId)
                .WithMessage("Unblocker ID and Unblocked ID cannot be the same.");
        }
    }
}