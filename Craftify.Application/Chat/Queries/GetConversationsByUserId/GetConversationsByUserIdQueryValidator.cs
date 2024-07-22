using FluentValidation;

namespace Craftify.Application.Chat.Queries.GetConversationsByUserId
{
    public class GetConversationsByUserIdQueryValidator : AbstractValidator<GetConversationsByUserIdQuery>
    {
        public GetConversationsByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}