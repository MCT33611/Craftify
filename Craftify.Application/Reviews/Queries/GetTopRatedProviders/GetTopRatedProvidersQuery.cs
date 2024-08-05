using Craftify.Application.Reviews.Common;
using MediatR;
using System.Collections.Generic;

namespace Craftify.Application.Providers.Queries.GetTopRatedProviders
{
    public class GetTopRatedProvidersQuery : IRequest<List<TopRatedProviderDto>>
    {
        public int Count { get; set; } = 10; // Default to top 10 providers
    }
}