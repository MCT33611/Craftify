using FluentValidation;

namespace Craftify.Application.Chat.Queries.GetMessagesByConversationId
{
    public class GetMessagesByConversationIdQueryValidator : AbstractValidator<GetMessagesByConversationIdQuery>
    {
        public GetMessagesByConversationIdQueryValidator()
        {
            RuleFor(x => x.ConversationId).NotEmpty();
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}