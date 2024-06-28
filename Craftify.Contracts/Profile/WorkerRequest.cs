
namespace Craftify.Contracts.Profile
{
    public record WorkerRequest(

        string ServiceTitle,

        string? LogoUrl,

        string? Description,

        string? CertificationUrl,

        string? Skills,

        DateTime HireDate,

        decimal PerHourPrice,

        bool Approved

        );
}
