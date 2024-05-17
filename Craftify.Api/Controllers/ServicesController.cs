using Craftify.Application.Category.Queries.GetAllCategory;
using Craftify.Application.Category.Queries.GetCategory;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Application.Service.Commands.CreateService;
using Craftify.Application.Service.Commands.DeleteService;
using Craftify.Application.Service.Commands.UpdateService;
using Craftify.Application.Service.Queries.GetAllService;
using Craftify.Application.Service.Queries.GetService;
using Craftify.Contracts.Service;
using Craftify.Domain.Constants;
using ErrorOr;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Craftify.Api.Controllers
{
    [Authorize(Roles = $"{AppConstants.Role_Admin},{ AppConstants.Role_Worker}")]
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController(
        IMapper _mapper,
        IMediator _mediator
        ) : ApiController
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            var query = _mapper.Map<GetAllServiceQuery>(new { });
            var result = _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var query = _mapper.Map<GetServiceQuery>(new { id });
            var result = _mediator.Send(query);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateService(ServiceRequest request)
        {
            var command = _mapper.Map<CreateServiceCommand>(request);
            var serviceId = await _mediator.Send(command);
            return CreatedAtAction("GetService", new { id = serviceId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(Guid id, ServiceRequest request)
        {
            TypeAdapterConfig<ServiceRequest, UpdateServiceCommand>.NewConfig().
                Map(dest => dest.Id, src => id);

            var command = _mapper.Map<UpdateServiceCommand>(request);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            await _mediator.Send(new DeleteServiceCommand(id));
            return NoContent();
        }
    }
}
