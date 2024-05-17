namespace Craftify.Domain.Entities
{
    public class Service
    {
        public Guid Id { get; set; }

        public Guid ProviderId { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public Decimal? Price { get; set; }

        public bool Availability { get; set; }

        public string ZipCode { get; set; } = null!;

    }
}
