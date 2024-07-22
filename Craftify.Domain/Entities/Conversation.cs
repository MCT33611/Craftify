using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Craftify.Domain.Entities
{
    public class Conversation
    {

        public Guid Id { get; set; }
        public string RoomId { get; set; } = null!;
        public Guid PeerOneId { get; set; }
        public User PeerOne { get; set; }
        public Guid PeerTwoId { get; set; }
        public User PeerTwo { get; set; }
        public bool IsBlocked { get; set; } // For blocking functionality
        public Guid? BlockerId { get; set; }
        public DateTime LastActivityTimestamp { get; set; } // To track conversation activity
        public ICollection<Message> Messages { get; set; } // Navigation property for messages
    }
}
