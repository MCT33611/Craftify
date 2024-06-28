using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Common.Interfaces.Service;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Profile.Commands.UploadWorkerDoc
{
    public class UploadWorkerDocCommandHandler(
        ICloudinaryService _cloudinaryService
        ) : IRequestHandler<UploadWorkerDocCommand, ErrorOr<string>>
    {
        public async Task<ErrorOr<string>> Handle(UploadWorkerDocCommand command, CancellationToken cancellationToken)
        {
            if (command.File == null || command.File.Length == 0)
                return Errors.User.InvaildCredetial;

            var fileUrl = await _cloudinaryService.UploadAsync(command.File, "worker_docs");

            return fileUrl;
        }
    }
}
