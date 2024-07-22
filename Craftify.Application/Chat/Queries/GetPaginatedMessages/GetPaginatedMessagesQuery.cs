using MediatR;
using Craftify.Application.Chat.Common;

namespace Craftify.Application.Chat.Queries.GetPaginatedMessages
{
    public record GetPaginatedMessagesQuery(Guid ConversationId, int Page, int PageSize) : IRequest<PaginatedMessageResult>;
}

public class PaginatedMessageResult
{
    public List<MessageResult> Messages { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}