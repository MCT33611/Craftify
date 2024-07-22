using MediatR;
using Craftify.Application.Chat.Common;

namespace Craftify.Application.Chat.Queries.SearchMessages
{
    public record SearchMessagesQuery(Guid ConversationId, string SearchTerm) : IRequest<List<MessageResult>>;
}