using System.ComponentModel.DataAnnotations;

namespace Craftify.Application.Chat.Commands.CreateConversation
{
    public class CreateConversationRequest
    {
        [Required]
        public Guid UserId1 { get; set; }

        [Required]
        public Guid UserId2 { get; set; }
    }
}