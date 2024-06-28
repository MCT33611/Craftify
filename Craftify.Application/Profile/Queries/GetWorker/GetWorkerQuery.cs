using Craftify.Application.Profile.Common;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Profile.Queries.GetWorker
{
    public record GetWorkerQuery(
        Guid Id
        ):IRequest<ErrorOr<WorkerResult>>;
}
