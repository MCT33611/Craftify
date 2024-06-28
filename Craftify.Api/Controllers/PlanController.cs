using Craftify.Application.Plan.Commands.CreatePlan;
using Craftify.Application.Plan.Commands.DeletePlan;
using Craftify.Application.Plan.Commands.UpdatePlan;
using Craftify.Application.Plan.Queries.GetAllPlan;
using Craftify.Application.Plan.Queries.GetPlan;
using Craftify.Contracts.Plan;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Craftify.Api.Controllers
{
    //[Authorize(Roles = AppConstants.Role_Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController(
        IMapper _mapper,
        IMediator _mediator
        ) : ApiController
    {


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPlanQuery());
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetPlanQuery(id);
            var result = await _mediator.Send(query);
            if(result.IsError)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePlan(PlanRequest request)
        {

            var command = _mapper.Map<CreatePlanCommand>(request);
            var result = await _mediator.Send(command);
            if (result.IsError)
                return BadRequest(result.Errors);
            return Ok( new { id = result.Value });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlan(Guid id, PlanRequest request)
        {
            TypeAdapterConfig<PlanRequest, UpdatePlanCommand>.NewConfig().
                Map(dest => dest.Id, src => id);

            var command = _mapper.Map<UpdatePlanCommand>(request);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlan(Guid id)
        {
            await _mediator.Send(new DeletePlanCommand(id));
            return NoContent();
        }
    }
}
