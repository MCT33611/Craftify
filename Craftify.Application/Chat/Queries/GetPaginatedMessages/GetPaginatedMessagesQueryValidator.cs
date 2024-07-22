using FluentValidation;

namespace Craftify.Application.Chat.Queries.GetPaginatedMessages
{
    public class GetPaginatedMessagesQueryValidator : AbstractValidator<GetPaginatedMessagesQuery>
    {
        public GetPaginatedMessagesQueryValidator()
        {
            RuleFor(x => x.ConversationId)
                .NotEmpty().WithMessage("ConversationId is required.");

            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Page must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("PageSize must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("PageSize must not exceed 100.");
        }
    }
}