
using Craftify.Application.Providers.Queries.GetTopRatedProviders;
using Craftify.Application.Ratings.Commands.CreateRating;
using Craftify.Application.Ratings.Queries.GetAverageRatingForProvider;
using Craftify.Application.Reviews.Commands.CreateRating;
using Craftify.Application.Reviews.Commands.CreateReview;
using Craftify.Application.Reviews.Common;
using Craftify.Application.Reviews.Queries.GetAllReviewsWithRatings;
using Craftify.Application.Reviews.Queries.GetRecentReviews;
using Craftify.Application.Reviews.Queries.GetReviewById;
using Craftify.Application.Reviews.Queries.GetReviewsByBookingId;
using Craftify.Application.Reviews.Queries.GetReviewsByCustomerId;
using Craftify.Application.Reviews.Queries.GetReviewsByProviderId;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Craftify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewRatingController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ReviewRatingController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("review")]
        public async Task<ActionResult<CreateReviewResult>> CreateReview([FromBody] CreateReviewCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("rating")]
        public async Task<ActionResult<CreateRatingResult>> CreateRating([FromBody] CreateRatingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<ReviewsWithRatingsVm>> GetAllReviewsWithRatings([FromQuery] GetAllReviewsWithRatingsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("provider/{providerId}/average-rating")]
        public async Task<ActionResult<ProviderAverageRatingVm>> GetAverageRatingForProvider(Guid providerId)
        {
            var query = new GetAverageRatingForProviderQuery { ProviderId = providerId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("recent")]
        public async Task<ActionResult<RecentReviewDto[]>> GetRecentReviews([FromQuery] GetRecentReviewsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{reviewId}")]
        public async Task<ActionResult<ReviewDetailVm>> GetReviewById(Guid reviewId)
        {
            var query = new GetReviewByIdQuery { Id = reviewId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("booking/{bookingId}")]
        public async Task<ActionResult<ReviewDto[]>> GetReviewsByBookingId(Guid bookingId)
        {
            var query = new GetReviewsByBookingIdQuery { BookingId = bookingId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<CustomerReviewDto[]>> GetReviewsByCustomerId(Guid customerId)
        {
            var query = new GetReviewsByCustomerIdQuery { CustomerId = customerId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("provider/{providerId}")]
        public async Task<ActionResult<ProviderReviewsVm>> GetReviewsByProviderId(Guid providerId)
        {
            var query = new GetReviewsByProviderIdQuery { ProviderId = providerId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("top-rated-providers")]
        public async Task<ActionResult<TopRatedProviderDto[]>> GetTopRatedProviders([FromQuery] GetTopRatedProvidersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}