using FluentValidation;

namespace Craftify.Application.Chat.Queries.GetMediaByType
{
    public class GetMediaByTypeQueryValidator : AbstractValidator<GetMediaByTypeQuery>
    {
        public GetMediaByTypeQueryValidator()
        {
            RuleFor(x => x.ConversationId)
                .NotEmpty().WithMessage("ConversationId is required.");

            RuleFor(x => x.MediaType)
                .IsInEnum().WithMessage("Invalid MediaType.");
        }
    }
}