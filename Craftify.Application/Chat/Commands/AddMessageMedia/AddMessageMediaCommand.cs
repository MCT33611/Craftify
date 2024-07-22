using Craftify.Application.Chat.Common;
using Craftify.Domain.Enums;
using MediatR;

namespace Craftify.Application.Chat.Commands.AddMessageMedia
{
    public class AddMessageMediaCommand : IRequest<MessageMediaResult>
    {
        public Guid MessageId { get; set; }
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long FileSize { get; set; }
        public string StoragePath { get; set; } = null!;
        public MediaType Type { get; set; }
    }
}