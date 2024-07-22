using Craftify.Domain.Enums;

namespace Craftify.Application.Chat.Common.Dtos
{

    public class MessageMediaDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public string StoragePath { get; set; }
        public MediaType Type { get; set; }
    }
}
