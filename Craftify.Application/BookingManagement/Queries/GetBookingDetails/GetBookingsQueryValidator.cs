using Craftify.Application.Profile.Queries.GetProfile;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.BookingManagement.Queries.GetBookingDetails
{
    public class GetBookingsQueryValidator : AbstractValidator<GetBookingsQuery>
    {
        public GetBookingsQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }

    }
}
