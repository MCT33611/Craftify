using Craftify.Application.Ratings.Commands.CreateRating;
using Craftify.Application.Reviews.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Reviews.Commands.CreateRating
{
    public class CreateRatingCommand : IRequest<CreateRatingResult>
    {
        public Guid ReviewId { get; set; }
        public int OverallScore { get; set; }
        public int PunctualityScore { get; set; }
        public int ProfessionalismScore { get; set; }
        public int QualityScore { get; set; }
    }
}
