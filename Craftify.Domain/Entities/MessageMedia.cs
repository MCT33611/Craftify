using Craftify.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Craftify.Domain.Entities
{
    public class MessageMedia
    {
        public Guid Id { get; set; }
        [ForeignKey(nameof(Message))]
        public Guid MessageId { get; set; }
        [AllowNull]
        public Message Message { get; set; }
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long FileSize { get; set; } 
        public string CdnUrl { get; set; } = null!;
        public MediaType Type { get; set; } 
    }
}
