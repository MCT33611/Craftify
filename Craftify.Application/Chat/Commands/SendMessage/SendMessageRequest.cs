using Craftify.Domain.Entities;
using Craftify.Domain.Enums;
using Microsoft.AspNetCore.Http;
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

        public List<MessageMedia> Media { get; set; } = new List<MessageMedia>();
    }

}