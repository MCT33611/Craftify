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
namespace Craftify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllBookingsQuery());
            return Ok(result.Value);
        }

        [HttpPost("booking")]
        public async Task<IActionResult> Booking (BookingRequest request)
        {
            var command = _mapper.Map<BookingCommand>(request);
            var result = await _mediator.Send(command);
            if (result.IsError)
                return BadRequest(result.Errors);
            return Ok(new { id = result.Value });
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateBookingDetails(Guid Id, BookingStatus status)
        {
            var command = new UpdateBookingCommand(Id,status);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
