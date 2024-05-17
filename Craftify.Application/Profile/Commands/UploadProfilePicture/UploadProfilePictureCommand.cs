using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Profile.Commands.UploadProfilePicture
{
    public record UploadProfilePictureCommand(Guid Id, IFormFile File) : IRequest<ErrorOr<string>>;
}
