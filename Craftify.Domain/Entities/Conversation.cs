using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Craftify.Domain.Entities
{
    public class Conversation
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(PeerOne))]
        public Guid PeerOneId { get; set; }

        [AllowNull]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public User PeerOne { get; set; }

        [ForeignKey(nameof(PeerTwo))]
        public Guid PeerTwoId { get; set; }

        [AllowNull]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public User PeerTwo { get; set; }


    }
}
