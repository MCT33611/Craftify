using MediatR;
using Craftify.Domain.Enums;
using Craftify.Application.Chat.Common;

namespace Craftify.Application.Chat.Queries.GetMediaByType
{
    public record GetMediaByTypeQuery(Guid ConversationId, MediaType MediaType) : IRequest<List<MessageMediaResult>>;
}