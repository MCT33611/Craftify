using Craftify.Application.Authentication.Commands.Register;
using Craftify.Application.Authentication.Common;
using Craftify.Application.Authentication.Queries.Login;
using Craftify.Contracts.Authentication;
using Mapster;

namespace Craftify.Api.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginQuery>();
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}
