using Craftify.Domain.Enums;

namespace Craftify.Api.Contracts.Chat
{
    public class AddMessageMediaRequest
    {
        public Guid MessageId { get; set; }
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long FileSize { get; set; }
        public string StoragePath { get; set; } = null!;
        public MediaType Type { get; set; }
    }
}