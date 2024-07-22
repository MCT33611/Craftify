using FluentValidation;

namespace Craftify.Application.Chat.Queries.GetMediaByMessageId
{
    public class GetMediaByMessageIdQueryValidator : AbstractValidator<GetMediaByMessageIdQuery>
    {
        public GetMediaByMessageIdQueryValidator()
        {
            RuleFor(x => x.MessageId)
                .NotEmpty().WithMessage("MessageId is required.");
        }
    }
}