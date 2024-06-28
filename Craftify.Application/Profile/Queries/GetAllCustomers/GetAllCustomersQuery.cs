using Craftify.Application.Profile.Common;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Profile.Queries.GetAllProfiles
{
    public record GetAllCustomersQuery() : IRequest<ErrorOr<IEnumerable<ProfileResult>>>;
}
