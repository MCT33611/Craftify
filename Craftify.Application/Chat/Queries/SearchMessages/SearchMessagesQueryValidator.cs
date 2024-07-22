using FluentValidation;

namespace Craftify.Application.Chat.Queries.SearchMessages
{
    public class SearchMessagesQueryValidator : AbstractValidator<SearchMessagesQuery>
    {
        public SearchMessagesQueryValidator()
        {
            RuleFor(x => x.ConversationId)
                .NotEmpty().WithMessage("ConversationId is required.");

            RuleFor(x => x.SearchTerm)
                .NotEmpty().WithMessage("SearchTerm is required.")
                .MinimumLength(3).WithMessage("SearchTerm must be at least 3 characters long.");
        }
    }
}