using Craftify.Application.Common.Interfaces.Persistence;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using ErrorOr;
using MediatR;
using Craftify.Domain.Common.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Craftify.Application.Common.Interfaces.Service;
using Craftify.Domain.Entities;
using static Craftify.Domain.Common.Errors.Errors;

namespace Craftify.Application.Profile.Commands.UpdateWorker
{
    public class UpdateWorkerCommandHandler(
        IUnitOfWork _unitOfWrok
        ) : IRequestHandler<UpdateWorkerCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            // Retrieve worker from repository
            Worker worker = _unitOfWrok.Worker.Get(w => w.Id == request.Id);
            if (worker == null)
                return Errors.User.InvaildCredetial;

            worker.ServiceTitle = request.Model.ServiceTitle ?? worker.ServiceTitle;
            worker.LogoUrl = request.Model.LogoUrl ?? worker.LogoUrl;
            worker.Description = request.Model.Description ?? worker.Description;
            worker.CertificationUrl = request.Model.CertificationUrl ?? worker.CertificationUrl;
            worker.Skills = request.Model.Skills ?? worker.Skills;
            worker.HireDate = request.Model.HireDate != default(DateTime) ? request.Model.HireDate : worker.HireDate;
            worker.PerHourPrice = request.Model.PerHourPrice != default(decimal) ? request.Model.PerHourPrice : worker.PerHourPrice;
            worker.Approved = request.Model.Approved;


            // Update worker in repository
            _unitOfWrok.Worker.Update(worker);
            _unitOfWrok.Save();
            return Unit.Value;

        }
    }
}
