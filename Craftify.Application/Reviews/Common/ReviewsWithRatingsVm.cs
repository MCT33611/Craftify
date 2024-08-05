using System;
using System.Collections.Generic;

namespace Craftify.Application.Reviews.Common
{
    public class ReviewsWithRatingsVm
    {
        public List<ReviewWithRatingDto> Reviews { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalReviews { get; set; }
    }

    public class ReviewWithRatingDto
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProviderId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public RatingDto Rating { get; set; }
    }


}