using System;

namespace Craftify.Application.Reviews.Common
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProviderId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public RatingDto Rating { get; set; }
    }

}