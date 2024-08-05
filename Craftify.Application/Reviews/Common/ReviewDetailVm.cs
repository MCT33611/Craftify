using System;

namespace Craftify.Application.Reviews.Common
{
    public class ReviewDetailVm
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProviderId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public RatingVm Rating { get; set; }
    }

    public class RatingVm
    {
        public int OverallScore { get; set; }
        public int PunctualityScore { get; set; }
        public int ProfessionalismScore { get; set; }
        public int QualityScore { get; set; }
    }
}