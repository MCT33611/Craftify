using Craftify.Application.Profile.Common;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Profile.Queries.GetAllWorkers
{
    public record GetAllWorkersQuery() : IRequest<ErrorOr<IEnumerable<WorkerResult>>>;
}
