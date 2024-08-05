using Craftify.Application.Reviews.Common;
using MediatR;
using System;

namespace Craftify.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<CreateReviewResult>
    {
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProviderId { get; set; }
        public string Comment { get; set; }
        public int OverallScore { get; set; }
        public int PunctualityScore { get; set; }
        public int ProfessionalismScore { get; set; }
        public int QualityScore { get; set; }
    }
}