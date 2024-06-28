using Craftify.Application.BookingManagement.Common;
using ErrorOr;
using MediatR;

namespace Craftify.Application.BookingManagement.Queries.GetBookingDetails
{
    public record GetBookingsQuery(
        Guid Id
        ) : IRequest<ErrorOr<BookingResult>>;
}
