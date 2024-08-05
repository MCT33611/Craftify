using System;

namespace Craftify.Application.Reviews.Common
{
    public class CustomerReviewDto
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public Guid ProviderId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public RatingDto Rating { get; set; }
    }


}