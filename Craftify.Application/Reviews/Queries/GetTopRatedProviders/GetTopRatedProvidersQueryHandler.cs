using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Reviews.Common;

namespace Craftify.Application.Providers.Queries.GetTopRatedProviders
{
    public class GetTopRatedProvidersQueryHandler
        : IRequestHandler<GetTopRatedProvidersQuery, List<TopRatedProviderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTopRatedProvidersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TopRatedProviderDto>> Handle(GetTopRatedProvidersQuery request, CancellationToken cancellationToken)
        {
            var topProviders = await _unitOfWork.ReviewRating.GetTopRatedProviders(request.Count);

            var result = new List<TopRatedProviderDto>();

            foreach (var provider in topProviders)
            {
                var averageRating = await _unitOfWork.ReviewRating.GetAverageRatingForProvider(provider.Id);
                var reviewCount = await _unitOfWork.ReviewRating.GetReviewCountForProvider(provider.Id);

                result.Add(new TopRatedProviderDto
                {
                    ProviderId = provider.Id,
                    Name = $"{provider.User.FirstName} {provider.User.LastName}",
                    ServiceTitle = provider.ServiceTitle,
                    AverageRating = averageRating,
                    ReviewCount = reviewCount,
                    PerHourPrice = provider.PerHourPrice,
                    SmallPreviewImageUrl = provider.SmallPreviewImageUrl
                });
            }

            return result;
        }
    }
}