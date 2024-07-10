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
using Craftify.Application.Profile.Commands.UpdateServiceProvider;
using Craftify.Domain.Entities;

namespace Craftify.Application.Profile.Commands.UpdateServiceProvider
{
    public class UpdateServiceProviderCommandHandler(
      IUnitOfWork _unitOfWork
      ) : IRequestHandler<UpdateServiceProviderCommand, ErrorOr<Unit>>
    {
        public async Task<ErrorOr<Unit>> Handle(UpdateServiceProviderCommand request, CancellationToken cancellationToken)
        {
            Worker provider = _unitOfWork.Worker.Get(p => p.Id == request.Id);
            if (provider == null)
                return Errors.User.InvaildCredetial;

            // Update user properties
            provider.ServiceTitle = !string.IsNullOrEmpty(request.Model.ServiceTitle) ? request.Model.ServiceTitle : provider.ServiceTitle;
            provider.Description = !string.IsNullOrEmpty(request.Model.Description) ? request.Model.Description : provider.Description;
            provider.CertificationUrl = !string.IsNullOrEmpty(request.Model.CertificationUrl) ? request.Model.CertificationUrl : provider.CertificationUrl;
            provider.Skills = !string.IsNullOrEmpty(request.Model.Skills) ? request.Model.Skills : provider.Skills;
            provider.HireDate = request.Model.HireDate != default ? request.Model.HireDate : provider.HireDate;
            provider.PerHourPrice = request.Model.PerHourPrice != 0 ? request.Model.PerHourPrice : provider.PerHourPrice;
            provider.Approved = request.Model.Approved;
            provider.LogoUrl = !string.IsNullOrEmpty(request.Model.LogoUrl) ? request.Model.LogoUrl : provider.LogoUrl;
            provider.SmallPreviewImageUrl = !string.IsNullOrEmpty(request.Model.SmallPreviewImageUrl) ? request.Model.SmallPreviewImageUrl : provider.SmallPreviewImageUrl;
            provider.MediumPreviewImageUrl = !string.IsNullOrEmpty(request.Model.MediumPreviewImageUrl) ? request.Model.MediumPreviewImageUrl : provider.MediumPreviewImageUrl;
            provider.LargePreviewImageUrl = !string.IsNullOrEmpty(request.Model.LargePreviewImageUrl) ? request.Model.LargePreviewImageUrl : provider.LargePreviewImageUrl;

            // Update user in repository
            _unitOfWork.Worker.Update(provider);
            await _unitOfWork.Save();
            return Unit.Value;
        }
    }
}
