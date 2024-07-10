using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Common.Interfaces.Service;
using Craftify.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Craftify.Application.BookingManagement.Commands.Booking
{
    public class BookingCommandHandler(
        IUnitOfWork _unitOfWork,
        IDateTimeProvider _dateTime
        ) : IRequestHandler<BookingCommand, ErrorOr<Guid>>
    {
        public async Task<ErrorOr<Guid>> Handle(BookingCommand request, CancellationToken cancellationToken)
        {
            Worker? worker = _unitOfWork.Worker.Get(w => w.Id == request.ProviderId);
            if(worker == null || !worker.Approved )
            {
                return Error.NotFound("can't find any approved service provider ");
            }

            User? customer = _unitOfWork.User.Get(c => c.Id == request.CustomerId);
            if (customer == null || customer.Blocked)
            {
                return Error.NotFound("can't find any  verifyed user ");
            }
            Domain.Entities.Booking booking = new()
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                ProviderId = request.ProviderId,
                Status = Domain.Enums.BookingStatus.Pending,
                Date = request.Date,
                BookedAt = _dateTime.UtcNow,
                Location = request.Location,
                LocationName = request.LocationName,
                WorkingTime = request.WorkingTime,
            };
            _unitOfWork.Booking.Add(booking);
            await _unitOfWork.Save();
            return booking.Id;
        }
    }
}
