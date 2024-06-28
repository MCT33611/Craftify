using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Craftify.Application.BookingManagement.Commands.Booking
{
    public class BookingCommandHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<BookingCommand, ErrorOr<Guid>>
    {
        public async Task<ErrorOr<Guid>> Handle(BookingCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            Domain.Entities.Booking booking = new()
            {
                Id = Guid.NewGuid(),
                
            };
            _unitOfWork.Booking.Add(booking);
            _unitOfWork.Save();
            return booking.Id;
        }
    }
}
