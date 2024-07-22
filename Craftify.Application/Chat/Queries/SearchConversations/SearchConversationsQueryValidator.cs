using FluentValidation;

namespace Craftify.Application.Chat.Queries.SearchConversations
{
    public class SearchConversationsQueryValidator : AbstractValidator<SearchConversationsQuery>
    {
        public SearchConversationsQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.SearchTerm)
                .NotEmpty().WithMessage("SearchTerm is required.")
                .MinimumLength(2).WithMessage("SearchTerm must be at least 2 characters long.");
        }
    }
}