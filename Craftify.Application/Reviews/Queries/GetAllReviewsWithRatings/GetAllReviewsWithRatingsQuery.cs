using Craftify.Application.Reviews.Common;
using MediatR;
using System.Collections.Generic;

namespace Craftify.Application.Reviews.Queries.GetAllReviewsWithRatings
{
    public class GetAllReviewsWithRatingsQuery : IRequest<ReviewsWithRatingsVm>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}