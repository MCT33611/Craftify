using FluentValidation;

namespace Craftify.Application.Chat.Queries.GetConversationByRoomId
{
    public class GetConversationByRoomIdQueryValidator : AbstractValidator<GetConversationByRoomIdQuery>
    {
        public GetConversationByRoomIdQueryValidator()
        {
            RuleFor(x => x.RoomId).NotEmpty();
        }
    }
}