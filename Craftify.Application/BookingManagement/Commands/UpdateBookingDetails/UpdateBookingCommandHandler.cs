using Craftify.Application.BookingManagement.Commands.Booking;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Craftify.Application.BookingManagement.Commands.UpdateBookingDetails
{
    public class UpdateBookingCommandHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<UpdateBookingCommand, ErrorOr<Guid>>
    {
        public async Task<ErrorOr<Guid>> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var booking = _unitOfWork.Booking.Get(b => b.Id == request.Id);

            if (booking == null)
            {
                return Error.NotFound("Booking not found");
            }

            // Update properties
            booking.Status = request.Status;

            _unitOfWork.Booking.Update(booking);
            _unitOfWork.Save();
            return booking.Id;
        }
    }
}
