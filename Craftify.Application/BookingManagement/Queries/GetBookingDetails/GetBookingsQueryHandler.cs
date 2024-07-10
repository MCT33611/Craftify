using Craftify.Application.BookingManagement.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
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

            var Bookings = _unitOfWork.Booking.Get(s => s.Id == query.Id,includedProperties: "Customer,Provider");
            return new BookingResult(
                    Bookings.Id,
                    Bookings.WorkingTime,
                    Bookings.Status,
                    Bookings.Date,
                    Bookings.BookedAt,
                    Bookings.Location,
                    Bookings.LocationName,
                    Bookings.CustomerId,
                    Bookings.Customer,
                    Bookings.ProviderId,
                    Bookings.Provider
                );
        }
    }
}

