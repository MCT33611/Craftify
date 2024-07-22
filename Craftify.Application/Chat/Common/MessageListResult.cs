using Craftify.Application.Common.Models;

namespace Craftify.Application.Chat.Common
{
    public class MessageListResult
    {
        public IEnumerable<MessageResult> Messages { get; set; } = new List<MessageResult>();
        public PaginationMetadata Metadata { get; set; } = new PaginationMetadata();
    }

}