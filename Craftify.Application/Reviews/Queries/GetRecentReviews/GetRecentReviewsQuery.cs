using Craftify.Application.Reviews.Common;
using MediatR;
using System.Collections.Generic;

namespace Craftify.Application.Reviews.Queries.GetRecentReviews
{
    public class GetRecentReviewsQuery : IRequest<List<RecentReviewDto>>
    {
        public int Count { get; set; } = 5; // Default to 5 recent reviews
    }
}