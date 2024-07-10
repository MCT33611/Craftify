using ErrorOr;
using MediatR;

namespace Craftify.Application.Profile.Commands.SubscribeProfile
{
    public record SubscribeProfileCommand(
         Guid UserId,
         Guid PlanId,
         string PaymentId,
         string? CertificationUrl,
         string? Skills,
         string? ServiceTitle,
         decimal PerHourPrice,
         string? Description
        ) : IRequest<ErrorOr<Unit>>;
}
