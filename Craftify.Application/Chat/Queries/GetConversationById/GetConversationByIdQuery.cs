using Craftify.Application.Chat.Common;
using MediatR;

namespace Craftify.Application.Chat.Queries.GetConversationById
{
    public class GetConversationByIdQuery : IRequest<ConversationResult>
    {
        public Guid ConversationId { get; set; }
    }
}