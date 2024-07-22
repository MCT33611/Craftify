using FluentValidation;

namespace Craftify.Application.Chat.Queries.GetLatestMessageByConversationId
{
    public class GetLatestMessageByConversationIdQueryValidator : AbstractValidator<GetLatestMessageByConversationIdQuery>
    {
        public GetLatestMessageByConversationIdQueryValidator()
        {
            RuleFor(x => x.ConversationId)
                .NotEmpty().WithMessage("ConversationId is required.");
        }
    }
}