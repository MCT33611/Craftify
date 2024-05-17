using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Common.Interfaces.Service;
using Craftify.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Craftify.Application.Profile.Commands.UploadProfilePicture
{
    public class UploadProfilePictureCommandHandler(
        IUnitOfWork _unitOfWork,
        ICloudinaryService _cloudinaryService
        ) : IRequestHandler<UploadProfilePictureCommand, ErrorOr<string>>
    {
        public async Task<ErrorOr<string>> Handle(UploadProfilePictureCommand command, CancellationToken cancellationToken)
        {
            if (command.File == null || command.File.Length == 0)
                return Errors.User.InvaildCredetial;

            var maxFileSizeInBytes = 10 * 1024 * 1024; // 10 MB
            if (command.File.Length > maxFileSizeInBytes)
                return Errors.User.InvaildCredetial;

            var imageUrl = await _cloudinaryService.UploadAsync(command.File, "profile_pictures");

            var user = _unitOfWork.User.GetUserById(command.Id);
            if (user == null)
                return Errors.User.InvaildCredetial;

            user.ProfilePicture = imageUrl;
            _unitOfWork.User.Update(user);

            return imageUrl;
        }
    }
}
