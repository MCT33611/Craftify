using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Craftify.Application.Reviews.Common
{
    public class RatingDto
    {
        public int OverallScore { get; set; }
        public int PunctualityScore { get; set; }
        public int ProfessionalismScore { get; set; }
        public int QualityScore { get; set; }
    }
}
