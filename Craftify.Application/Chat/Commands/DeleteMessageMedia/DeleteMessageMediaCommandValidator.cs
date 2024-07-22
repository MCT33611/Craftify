using FluentValidation;

namespace Craftify.Application.Chat.Commands.DeleteMessageMedia
{
    public class DeleteMessageMediaCommandValidator : AbstractValidator<DeleteMessageMediaCommand>
    {
        public DeleteMessageMediaCommandValidator()
        {
            RuleFor(x => x.MediaId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}