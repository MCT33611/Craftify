using Craftify.Application.BookingManagement.Common;
using Craftify.Application.Plan.Common;
using ErrorOr;
using MediatR;

namespace Craftify.Application.BookingManagement.Queries.GetAllBookings
{
    public record GetAllBookingsQuery(Guid userId) : IRequest<ErrorOr<IEnumerable<BookingResult>>>;
}
