using FluentValidation;

namespace Craftify.Application.Chat.Queries.GetUnreadMessagesCount
{
    public class GetUnreadMessagesCountQueryValidator : AbstractValidator<GetUnreadMessagesCountQuery>
    {
        public GetUnreadMessagesCountQueryValidator()
        {
            RuleFor(x => x.ConversationId)
                .NotEmpty().WithMessage("ConversationId is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}