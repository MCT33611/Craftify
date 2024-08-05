using Craftify.Application.Reviews.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace Craftify.Application.Reviews.Queries.GetReviewsByBookingId
{
    public class GetReviewsByBookingIdQuery : IRequest<List<ReviewDto>>
    {
        public Guid BookingId { get; set; }
    }
}