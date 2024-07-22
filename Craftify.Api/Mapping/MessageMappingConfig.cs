using Craftify.Application.Chat.Common;
using Craftify.Domain.Entities;
using Mapster;

namespace Craftify.Api.Mapping
{
    public class MessageMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Message, MessageResult>()
                .Map(dest => dest.Media, src => src.Media);

            config.NewConfig<MessageMedia, MessageMediaResult>();
        }
    }
}
