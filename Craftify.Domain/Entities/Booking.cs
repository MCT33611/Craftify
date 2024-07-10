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

        public DateTime Date { get; set; }

        public DateTime BookedAt { get; set; }


        public BookingStatus Status { get; set; }

        public string Location { get; set; } = null!;
        public string LocationName { get; set; } = null!;

        [ForeignKey(nameof(Customer))]
        public Guid CustomerId { get; set; }

        [AllowNull]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public User Customer { get; set; }

        [ForeignKey(nameof(Provider))]
        public Guid ProviderId { get; set; }

        [AllowNull]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Worker Provider {  get; set; }

    }
}
