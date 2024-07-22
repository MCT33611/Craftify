using MediatR;

namespace Craftify.Application.Chat.Queries.GetUnreadConversationsCount
{
    public class GetUnreadConversationsCountQuery : IRequest<int>
    {
        public Guid UserId { get; set; }
    }
}