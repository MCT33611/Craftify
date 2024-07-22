using Craftify.Application.Chat.Common.Dtos;
using Craftify.Domain.Enums;

namespace Craftify.Application.Chat.Commands.UpdateMessage
{
    public class UpdateMessageRequest
    {
        public Guid MessageId { get; set; }
        public string NewContent { get; set; }
        public MessageType Type { get; set; }
        public List<MessageMediaDto> UpdatedMedia { get; set; }
    }
}