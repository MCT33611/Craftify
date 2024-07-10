using FluentValidation;


namespace Craftify.Application.BookingManagement.Commands.Booking
{
    public class BookingCommandValidator : AbstractValidator<BookingCommand>
    {
        public BookingCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.WorkingTime).NotEmpty();
            RuleFor(x => x.ProviderId).NotEmpty();
            RuleFor(x => x.Location).NotEmpty();
            RuleFor(x => x.LocationName).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
        }
    }

}
