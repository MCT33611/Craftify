using Craftify.Application.BookingManagement.Queries.GetAllBookings;
using Craftify.Application.BookingManagement.Queries.GetBookingDetails;
using Craftify.Application.Plan.Commands.CreatePlan;
using Craftify.Application.Plan.Commands.UpdatePlan;
using Craftify.Application.Plan.Queries.GetAllPlan;
using Craftify.Application.Plan.Queries.GetPlan;
using Craftify.Contracts.Plan;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Craftify.Contracts.Booking;
using Craftify.Application.BookingManagement.Commands.Booking;
using Craftify.Application.BookingManagement.Commands.UpdateBookingDetails;
using Craftify.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using CloudinaryDotNet.Actions;
using Craftify.Domain.Constants;
using ErrorOr;
namespace Craftify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingController(
        ISender _mediator,
        IMapper _mapper
        ) : ApiController
    {

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            var query = new GetBookingsQuery(Id);
            var result = await _mediator.Send(query);
            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid userId)
        {
            var result = await _mediator.Send(new GetAllBookingsQuery(userId));
            return Ok(result.Value);
        }

        [Authorize(Roles =AppConstants.Role_Customer)]
        [HttpPost("book")]
        public async Task<IActionResult> Book (BookingRequest request)
        {
            var command = _mapper.Map<BookingCommand>(request);
            var result = await _mediator.Send(command);
            if (result.IsError)
                return BadRequest(result.Errors);
            return Ok(new { id = result.Value });
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateBookingDetails(Guid Id, BookingRequest booking)
        {
            TypeAdapterConfig<BookingRequest, UpdateBookingCommand>.NewConfig().
                Map(dest => dest.Id, src => Id);
            var command = _mapper.Map<UpdateBookingCommand>(booking);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
