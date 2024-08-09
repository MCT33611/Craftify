
using Craftify.Domain.Enums;
using FluentValidation;

namespace Craftify.Application.Chat.Commands.SendMessage
{
    public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
    {
        public SendMessageCommandValidator()
        {
            RuleFor(x => x.ConversationId).NotEmpty().WithMessage("ConversationId is required.");
            RuleFor(x => x.FromId).NotEmpty().WithMessage("FromId is required.");
            RuleFor(x => x.ToId).NotEmpty().WithMessage("ToId is required.");
            RuleFor(x => x.Content).NotEmpty().When(x => x.Type == MessageType.Text || x.Type == MessageType.Mixed)
                .WithMessage("Content is required for text and mixed message types.");
            RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid message type.");
        }
    }


}