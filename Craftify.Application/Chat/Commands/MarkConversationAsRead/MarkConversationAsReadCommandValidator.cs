using FluentValidation;

namespace Craftify.Application.Chat.Commands.MarkConversationAsRead
{
    public class MarkConversationAsReadCommandValidator : AbstractValidator<MarkConversationAsReadCommand>
    {
        public MarkConversationAsReadCommandValidator()
        {
            RuleFor(x => x.ConversationId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}