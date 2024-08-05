using System;

namespace Craftify.Application.Reviews.Common
{
    public class TopRatedProviderDto
    {
        public Guid ProviderId { get; set; }
        public string Name { get; set; }
        public string ServiceTitle { get; set; }
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
        public decimal PerHourPrice { get; set; }
        public string SmallPreviewImageUrl { get; set; }
    }
}