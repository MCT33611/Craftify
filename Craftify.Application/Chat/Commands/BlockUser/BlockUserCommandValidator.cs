using FluentValidation;

namespace Craftify.Application.Chat.Commands.BlockUser
{
    public class BlockUserCommandValidator : AbstractValidator<BlockUserCommand>
    {
        public BlockUserCommandValidator()
        {
            RuleFor(x => x.BlockerId).NotEmpty();
            RuleFor(x => x.BlockedId).NotEmpty();
            RuleFor(x => x.BlockerId).NotEqual(x => x.BlockedId)
                .WithMessage("A user cannot block themselves");
        }
    }
}