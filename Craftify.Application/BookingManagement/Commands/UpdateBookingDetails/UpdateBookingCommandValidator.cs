using FluentValidation;


namespace Craftify.Application.BookingManagement.Commands.UpdateBookingDetails
{
    public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
    {
        public UpdateBookingCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

}
