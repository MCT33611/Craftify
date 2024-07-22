using MediatR;

namespace Craftify.Application.Chat.Commands.MarkConversationAsRead
{
    public class MarkConversationAsReadCommand : IRequest<bool>
    {
        public Guid ConversationId { get; set; }
        public Guid UserId { get; set; }
    }
}