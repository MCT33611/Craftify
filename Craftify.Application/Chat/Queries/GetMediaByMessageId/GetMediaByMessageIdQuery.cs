using MediatR;
using Craftify.Application.Chat.Common;

namespace Craftify.Application.Chat.Queries.GetMediaByMessageId
{
    public record GetMediaByMessageIdQuery(Guid MessageId) : IRequest<List<MessageMediaResult>>;
}