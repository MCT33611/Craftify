using System;
using System.Collections.Generic;

namespace Craftify.Application.Reviews.Common
{
    public class ProviderReviewsVm
    {
        public Guid ProviderId { get; set; }
        public List<ProviderReviewDto> Reviews { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
    }

    public class ProviderReviewDto
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public RatingDto Rating { get; set; }
    }

}