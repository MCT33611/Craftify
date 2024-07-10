
namespace Craftify.Contracts.Profile
{
    public record WorkerRequest(

        string ServiceTitle,

        string? Description,


        string? Skills,


        decimal PerHourPrice,


        string? LogoUrl ,
        string? SmallPreviewImageUrl ,
        string? MediumPreviewImageUrl ,
        string? LargePreviewImageUrl 
        );
}
