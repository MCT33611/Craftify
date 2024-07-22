using FluentValidation;

namespace Craftify.Application.Chat.Commands.DeleteMessage
{
    public class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
    {
        public DeleteMessageCommandValidator()
        {
            RuleFor(x => x.MessageId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}