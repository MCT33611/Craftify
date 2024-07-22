using FluentValidation;

namespace Craftify.Application.Chat.Queries.GetConversationById
{
    public class GetConversationByIdQueryValidator : AbstractValidator<GetConversationByIdQuery>
    {
        public GetConversationByIdQueryValidator()
        {
            RuleFor(x => x.ConversationId).NotEmpty();
        }
    }
}