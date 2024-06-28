using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Domain.Entities
{
    public class Subscription
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(Worker))]
        public Guid WorkerId { get; set; }
        [AllowNull]
        public Worker Worker { get; set; }

        [ForeignKey(nameof(Plan))]
        public Guid PlanId { get; set; }
        [AllowNull]
        public Plan Plan { get; set; }

        public string PaymentId { get; set; } = null!;

        public DateTime ExpireAt { get; set; }
    }
}
