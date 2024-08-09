using Craftify.Application.Chat.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Craftify.Application.Chat.Commands.UploadMedia
{
    public class UploadMediaCommand : IRequest<List<MessageMediaResult>>
    {
        public IFormFileCollection MediaFiles { get; set; }
    }
}