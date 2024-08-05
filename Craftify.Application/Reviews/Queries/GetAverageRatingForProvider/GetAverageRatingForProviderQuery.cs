using Craftify.Application.Reviews.Common;
using MediatR;
using System;

namespace Craftify.Application.Ratings.Queries.GetAverageRatingForProvider
{
    public class GetAverageRatingForProviderQuery : IRequest<ProviderAverageRatingVm>
    {
        public Guid ProviderId { get; set; }
    }
}