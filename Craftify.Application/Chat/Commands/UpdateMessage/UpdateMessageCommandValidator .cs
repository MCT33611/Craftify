using FluentValidation;
using Craftify.Application.Chat.Common.Dtos;

namespace Craftify.Application.Chat.Commands.UpdateMessage
{
    public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageCommandValidator()
        {
            RuleFor(x => x.MessageId).NotEmpty();
            RuleFor(x => x.NewContent).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.Type).IsInEnum();
            RuleForEach(x => x.UpdatedMedia).SetValidator(new MessageMediaDtoValidator());
        }
    }

    public class MessageMediaDtoValidator : AbstractValidator<MessageMediaDto>
    {
        public MessageMediaDtoValidator()
        {
            RuleFor(x => x.FileName).NotEmpty().MaximumLength(255);
            RuleFor(x => x.ContentType).NotEmpty().MaximumLength(100);
            RuleFor(x => x.FileSize).GreaterThan(0);
            RuleFor(x => x.StoragePath).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Type).IsInEnum();
        }
    }
}