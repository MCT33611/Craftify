using FluentValidation;

namespace Craftify.Application.Chat.Commands.AddMessageMedia
{
    public class AddMessageMediaCommandValidator : AbstractValidator<AddMessageMediaCommand>
    {
        public AddMessageMediaCommandValidator()
        {
            RuleFor(x => x.MessageId).NotEmpty();
            RuleFor(x => x.FileName).NotEmpty().MaximumLength(255);
            RuleFor(x => x.ContentType).NotEmpty().MaximumLength(100);
            RuleFor(x => x.FileSize).GreaterThan(0);
            RuleFor(x => x.StoragePath).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.Type).IsInEnum();
        }
    }
}