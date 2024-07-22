using Craftify.Application.Chat.Common.Dtos;
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
            RuleFor(x => x.Media).NotEmpty().When(x => x.Type == MessageType.Media || x.Type == MessageType.Mixed)
                .WithMessage("Media is required for media and mixed message types.");

            RuleForEach(x => x.Media).SetValidator(new MessageMediaDtoValidator());
        }
    }

    public class MessageMediaDtoValidator : AbstractValidator<MessageMediaDto>
    {
        public MessageMediaDtoValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().WithMessage("FileName is required.");
            RuleFor(x => x.ContentType).NotEmpty().WithMessage("ContentType is required.");
            RuleFor(x => x.FileSize).GreaterThan(0).WithMessage("FileSize must be greater than 0.");
            RuleFor(x => x.StoragePath).NotEmpty().WithMessage("StoragePath is required.");
            RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid media type.");
        }
    }
}