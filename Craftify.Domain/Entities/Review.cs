using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Craftify.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(Booking))]
        public Guid BookingId { get; set; }

        [AllowNull]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Booking Booking { get; set; }

        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }

        [AllowNull]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public User Customer { get; set; }

        [ForeignKey(nameof(Provider))]
        public Guid ProviderId { get; set; }

        [AllowNull]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Worker Provider { get; set; }

        [Required]
        [StringLength(1000)]
        public string Comment { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Rating Rating { get; set; } = null!;
    }
}
