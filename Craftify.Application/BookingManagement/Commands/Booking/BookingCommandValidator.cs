using FluentValidation;


namespace Craftify.Application.BookingManagement.Commands.Booking
{
    public class BookingCommandValidator : AbstractValidator<BookingCommand>
    {
        public BookingCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.WorkingTime).NotEmpty();
            RuleFor(x => x.ProviderId).NotEmpty();
        }
    }

}
