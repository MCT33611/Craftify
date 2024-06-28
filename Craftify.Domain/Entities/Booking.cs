using Craftify.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Craftify.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int WorkingTime { get; set; }

        public BookingStatus Status { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [AllowNull]
        public User User { get; set; }

        [ForeignKey(nameof(Provider))]
        public Guid ProviderId { get; set; }

        [AllowNull]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Worker Provider {  get; set; }
    }
}
