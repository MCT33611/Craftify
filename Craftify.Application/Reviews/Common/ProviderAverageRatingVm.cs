using System;

namespace Craftify.Application.Reviews.Common
{
    public class ProviderAverageRatingVm
    {
        public Guid ProviderId { get; set; }
        public double AverageOverallRating { get; set; }
        public double AveragePunctualityRating { get; set; }
        public double AverageProfessionalismRating { get; set; }
        public double AverageQualityRating { get; set; }
        public int TotalReviews { get; set; }
    }
}