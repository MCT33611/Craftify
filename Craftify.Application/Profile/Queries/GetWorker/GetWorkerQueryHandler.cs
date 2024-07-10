using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Profile.Common;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Profile.Queries.GetWorker
{
    public class GetWorkerQueryHandler(
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<GetWorkerQuery, ErrorOr<WorkerResult>>
    {
        public async Task<ErrorOr<WorkerResult>> Handle(GetWorkerQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var worker = _unitOfWork.Worker.Get(u => u.Id == request.Id,includedProperties:"User");
            if (worker == null)
                return Errors.User.InvaildCredetial;

            return new WorkerResult
            {
                Id = worker.Id,
                UserId = worker.UserId,
                User = worker.User,
                ServiceTitle =worker.ServiceTitle,
                Description = worker.Description,
                CertificationUrl = worker.CertificationUrl,
                Skills = worker.Skills,
                HireDate = worker.HireDate,
                PerHourPrice = worker.PerHourPrice,
                Approved = worker.Approved,
                LogoUrl = worker.LogoUrl,
                SmallPreviewImageUrl = worker.SmallPreviewImageUrl,
                MediumPreviewImageUrl = worker.MediumPreviewImageUrl,
                LargePreviewImageUrl = worker.LargePreviewImageUrl
            };


        }
    }
}
