using Craftify.Application.BookingManagement.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using ErrorOr;
using MediatR;

namespace Craftify.Application.BookingManagement.Queries.GetBookingDetails
{
    public class GetBookingsQueryHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<GetBookingsQuery, ErrorOr<BookingResult>>
    {

        public async Task<ErrorOr<BookingResult>> Handle(GetBookingsQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var Bookings = _unitOfWork.Booking.Get(s => s.Id == query.Id);
            return new BookingResult(
                    Bookings.Id,
                    Bookings.WorkingTime,
                    Bookings.Status,
                    Bookings.UserId,
                    Bookings.User,
                    Bookings.ProviderId,
                    Bookings.Provider
                );
        }
    }
}

