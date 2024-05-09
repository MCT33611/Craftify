using Craftify.Domain.Entities;

namespace Craftify.Application.Services.Authentication
{
    public record AuthenticationResult
    (
        User User,
        string Token

    );
}
