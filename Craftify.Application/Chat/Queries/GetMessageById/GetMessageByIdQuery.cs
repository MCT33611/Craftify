using Craftify.Application.Chat.Common;
using MediatR;

namespace Craftify.Application.Chat.Queries.GetMessageById
{
    public class GetMessageByIdQuery : IRequest<MessageResult>
    {
        public Guid MessageId { get; set; }
    }
}