using Craftify.Application.Chat.Common;
using MediatR;

namespace Craftify.Application.Chat.Queries.GetLatestMessageByConversationId
{
    public record GetLatestMessageByConversationIdQuery(Guid ConversationId) : IRequest<MessageResult>;
}