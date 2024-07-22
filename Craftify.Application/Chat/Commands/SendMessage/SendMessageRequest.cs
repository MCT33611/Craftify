using Craftify.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Craftify.Application.Chat.Commands.SendMessage
{
    public class SendMessageRequest
    {
        [Required]
        public Guid ConversationId { get; set; }

        [Required]
        public Guid FromId { get; set; }

        [Required]
        public Guid ToId { get; set; }

        public string Content { get; set; }

        [Required]
        public MessageType Type { get; set; }

        public List<MessageMediaRequest> Media { get; set; }
    }

    public class MessageMediaRequest
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long FileSize { get; set; }

        [Required]
        public string StoragePath { get; set; }

        [Required]
        public MediaType Type { get; set; }
    }
}