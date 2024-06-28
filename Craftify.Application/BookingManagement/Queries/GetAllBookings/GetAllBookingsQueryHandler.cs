using Craftify.Application.BookingManagement.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using ErrorOr;
using MediatR;

namespace Craftify.Application.BookingManagement.Queries.GetAllBookings
{
    public class GetAllBookingsQueryHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<GetAllBookingsQuery, ErrorOr<IEnumerable<BookingResult>>>
    {

        public async Task<ErrorOr<IEnumerable<BookingResult>>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            try
            {
                var Bookings = _unitOfWork.Booking.GetAll().ToList();
                var BookingResults = Bookings.Select(Booking => new BookingResult
                (
                    Booking.Id,
                    Booking.WorkingTime,
                    Booking.Status,
                    Booking.UserId,
                    Booking.User,
                    Booking.ProviderId,
                    Booking.Provider
                ));

                return BookingResults.ToList();
            }
            catch (Exception)
            {
                return Error.NotFound();
            }
        }
    }
}

