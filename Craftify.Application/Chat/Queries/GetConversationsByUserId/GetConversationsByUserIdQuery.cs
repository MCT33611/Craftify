using Craftify.Application.Chat.Common;
using MediatR;

namespace Craftify.Application.Chat.Queries.GetConversationsByUserId
{
    public class GetConversationsByUserIdQuery : IRequest<List<ConversationResult>>
    {
        public Guid UserId { get; set; }
    }
}