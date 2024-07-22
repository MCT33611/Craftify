using CloudinaryDotNet.Actions;
using Craftify.Application.Chat.Common;
using Craftify.Domain.Entities;
using Mapster;

namespace Craftify.Api.Mapping
{
    public class ConversationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Conversation, ConversationResult>();
        }
    }
}
