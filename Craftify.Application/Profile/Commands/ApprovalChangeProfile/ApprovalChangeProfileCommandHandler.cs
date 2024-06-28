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

namespace Craftify.Application.Profile.Commands.ApprovalChangeProfile;

public class ApprovalChangeProfileCommandHandler(
    IUnitOfWork _unitOfWrok
    ) : IRequestHandler<ApprovalChangeProfileCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(ApprovalChangeProfileCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var worker = _unitOfWrok.Worker.Get(w => w.UserId == request.Id);
        if (worker == null)
            return Errors.User.InvaildCredetial;

        worker.Approved = !worker.Approved;

        _unitOfWrok.Worker.Update(worker);
        _unitOfWrok.Save();
        return Unit.Value;

    }
}
