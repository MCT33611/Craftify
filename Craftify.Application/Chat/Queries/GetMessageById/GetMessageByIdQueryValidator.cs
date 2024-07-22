using FluentValidation;

namespace Craftify.Application.Chat.Queries.GetMessageById
{
    public class GetMessageByIdQueryValidator : AbstractValidator<GetMessageByIdQuery>
    {
        public GetMessageByIdQueryValidator()
        {
            RuleFor(x => x.MessageId).NotEmpty();
        }
    }
}