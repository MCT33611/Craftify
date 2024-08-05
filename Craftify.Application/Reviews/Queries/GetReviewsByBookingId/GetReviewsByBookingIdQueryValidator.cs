using FluentValidation;

namespace Craftify.Application.Reviews.Queries.GetReviewsByBookingId
{
    public class GetReviewsByBookingIdQueryValidator : AbstractValidator<GetReviewsByBookingIdQuery>
    {
        public GetReviewsByBookingIdQueryValidator()
        {
            RuleFor(v => v.BookingId).NotEmpty();
        }
    }
}