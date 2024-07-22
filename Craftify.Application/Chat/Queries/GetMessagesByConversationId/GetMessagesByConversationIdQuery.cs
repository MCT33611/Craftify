using Craftify.Application.Chat.Common;
using MediatR;

namespace Craftify.Application.Chat.Queries.GetMessagesByConversationId
{
    public class GetMessagesByConversationIdQuery : IRequest<MessageListResult>
    {
        public Guid ConversationId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}