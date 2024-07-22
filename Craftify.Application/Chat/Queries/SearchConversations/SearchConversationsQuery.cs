using MediatR;
using Craftify.Application.Chat.Common;

namespace Craftify.Application.Chat.Queries.SearchConversations
{
    public record SearchConversationsQuery(Guid UserId, string SearchTerm) : IRequest<List<ConversationResult>>;
}