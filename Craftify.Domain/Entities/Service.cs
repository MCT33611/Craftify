using System.ComponentModel.DataAnnotations.Schema;

namespace Craftify.Domain.Entities
{
    public class Service
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(Provider))]
        public Guid ProviderId { get; set; }
        public User Provider { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public Decimal? Price { get; set; }

        public bool Availability { get; set; }

        public string ZipCode { get; set; } = null!;

    }
}
