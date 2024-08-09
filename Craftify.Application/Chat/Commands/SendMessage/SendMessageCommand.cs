using MediatR;
using Craftify.Application.Chat.Common;
using Craftify.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Craftify.Application.Chat.Commands.SendMessage
{
    public class SendMessageCommand : IRequest<MessageResult>
    {
        public Guid ConversationId { get; set; }
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public string Content { get; set; }
        public MessageType Type { get; set; }
        public List<MessageMediaResult> Media { get; set; }
    }

}