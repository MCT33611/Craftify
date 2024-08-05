using System;

namespace Craftify.Application.Ratings.Commands.CreateRating
{
    public class CreateRatingResult
    {
        public Guid RatingId { get; set; }
        public Guid ReviewId { get; set; }
    }
}