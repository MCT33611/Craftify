using Craftify.Application.Chat.Common;
using MediatR;

namespace Craftify.Application.Chat.Queries.GetConversationByRoomId
{
    public class GetConversationByRoomIdQuery : IRequest<ConversationResult>
    {
        public string RoomId { get; set; }
    }
}