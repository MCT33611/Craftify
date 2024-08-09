using Craftify.Application.Chat.Common;
using Craftify.Domain.Enums;
using MediatR;

namespace Craftify.Application.Chat.Commands.UpdateMessage
{
    public class UpdateMessageCommand : IRequest<MessageResult>
    {
        public Guid MessageId { get; set; }
        public string NewContent { get; set; }
        public MessageType Type { get; set; }
    }
}