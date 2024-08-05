using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Craftify.Domain.Entities
{
    public class Rating
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(Review))]
        public Guid ReviewId { get; set; }

        public Review Review { get; set; } = null!;

        [Range(1, 5)]
        public int OverallScore { get; set; }

        [Range(1, 5)]
        public int PunctualityScore { get; set; }

        [Range(1, 5)]
        public int ProfessionalismScore { get; set; }

        [Range(1, 5)]
        public int QualityScore { get; set; }
    }
}
