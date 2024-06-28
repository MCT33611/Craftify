using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Profile.Commands.UploadWorkerDoc
{
    public record UploadWorkerDocCommand(IFormFile File) : IRequest<ErrorOr<string>>;
}
