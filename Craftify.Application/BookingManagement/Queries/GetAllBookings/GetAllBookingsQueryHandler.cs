using Craftify.Application.BookingManagement.Common;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Constants;
using Craftify.Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

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
                User user = _unitOfWork.User.Get(u => u.Id == request.userId);
                if (user == null || user.Blocked)
                {
                    return Error.NotFound("can't find any valid user");
                }

                var Bookings = new List<Booking>();
                switch (user.Role)
                {
                    case AppConstants.Role_Admin:Bookings = _unitOfWork.Booking.GetAll(includedProperties: "Provider").ToList(); 
                        break;

                    case AppConstants.Role_Customer:Bookings = _unitOfWork.Booking.GetAll(b => b.CustomerId == user.Id, includedProperties: "Provider").ToList(); 
                        break;

                    case AppConstants.Role_Worker:
                        Worker worker = _unitOfWork.Worker.Get(w => w.UserId == user.Id);
                        Bookings = _unitOfWork.Booking.GetAll(b => b.ProviderId == worker.Id, includedProperties: "Provider").ToList(); 
                        break;

                }


                var BookingResults = Bookings.Select(Booking => {
                    var providerUser = _unitOfWork.User.Get(u => u.Id == Booking.Provider.UserId);
                    var customer = _unitOfWork.User.Get(u => u.Id == Booking.CustomerId);
                    Booking.Provider.User = providerUser;
                    return new BookingResult
                    (
                        Booking.Id,
                        Booking.WorkingTime,
                        Booking.Status,
                        Booking.Date,
                        Booking.BookedAt,
                        Booking.Location,
                        Booking.LocationName,
                        Booking.CustomerId,
                        customer,
                        Booking.ProviderId,
                        Booking.Provider
                    );
                }
                );

                

                return BookingResults.ToList();
            }
            catch (Exception)
            {
                return Error.NotFound();
            }
        }
    }
}

