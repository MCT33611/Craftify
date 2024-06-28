using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Contracts.Profile
{
    public record SubscriptionRequest
    (
         Guid UserId,
         Guid PlanId,
         string PaymentId,
         string? CertificationUrl,
         string? Skills,
         string? ServiceTitle,
         decimal PerHourPrice,
         string? LogoUrl,
         string? Description

    );
}
