using FluentValidation;

namespace Craftify.Application.Chat.Queries.GetUnreadConversationsCount
{
    public class GetUnreadConversationsCountQueryValidator : AbstractValidator<GetUnreadConversationsCountQuery>
    {
        public GetUnreadConversationsCountQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}