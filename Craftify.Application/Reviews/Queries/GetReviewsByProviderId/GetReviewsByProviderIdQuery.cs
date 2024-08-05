using Craftify.Application.Reviews.Common;
using MediatR;
using System;
using System.Collections.Generic;

namespace Craftify.Application.Reviews.Queries.GetReviewsByProviderId
{
    public class GetReviewsByProviderIdQuery : IRequest<ProviderReviewsVm>
    {
        public Guid ProviderId { get; set; }
    }
}