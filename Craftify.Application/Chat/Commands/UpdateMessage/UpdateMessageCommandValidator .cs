using FluentValidation;

namespace Craftify.Application.Chat.Commands.UpdateMessage
{
    public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageCommandValidator()
        {
            RuleFor(x => x.MessageId).NotEmpty();
            RuleFor(x => x.NewContent).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.Type).IsInEnum();
        }
    }


}