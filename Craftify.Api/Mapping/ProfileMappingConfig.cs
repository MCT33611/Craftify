using Craftify.Application.Profile.Commands.SubscribeProfile;
using Craftify.Contracts.Profile;
using Mapster;

namespace Craftify.Api.Mapping
{
    public class ProfileMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<SubscribeProfileCommand, SubscriptionRequest>()
                .Map(dest => dest, src => src.ServiceTitle);
        }
    }
}
