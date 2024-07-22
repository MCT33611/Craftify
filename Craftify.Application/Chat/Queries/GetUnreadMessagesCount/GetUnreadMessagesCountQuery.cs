using MediatR;

namespace Craftify.Application.Chat.Queries.GetUnreadMessagesCount
{
    public record GetUnreadMessagesCountQuery(Guid ConversationId, Guid UserId) : IRequest<int>;
}