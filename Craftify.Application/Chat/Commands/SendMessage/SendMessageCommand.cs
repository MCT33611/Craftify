using MediatR;
using Craftify.Application.Chat.Common;
using Craftify.Domain.Enums;
using Craftify.Application.Chat.Common.Dtos;

namespace Craftify.Application.Chat.Commands.SendMessage
{
    public class SendMessageCommand : IRequest<MessageResult>
    {
        public Guid ConversationId { get; set; }
        public Guid FromId { get; set; }
        public Guid ToId { get; set; }
        public string Content { get; set; }
        public MessageType Type { get; set; }
        public List<MessageMediaDto> Media { get; set; }
    }

}