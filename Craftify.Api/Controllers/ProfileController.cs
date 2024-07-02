using Craftify.Application.Profile.Commands.AccessChangeProfile;
using Craftify.Application.Profile.Commands.ApprovalChangeProfile;
using Craftify.Application.Profile.Commands.DeleteProfile;
using Craftify.Application.Profile.Commands.InitSubscribeProfile;
using Craftify.Application.Profile.Commands.SubscribeProfile;
using Craftify.Application.Profile.Commands.UpdateProfile;
using Craftify.Application.Profile.Commands.UpdateWorker;
using Craftify.Application.Profile.Commands.UploadProfilePicture;
using Craftify.Application.Profile.Commands.UploadWorkerDoc;
using Craftify.Application.Profile.Common;
using Craftify.Application.Profile.Queries.GetAllProfiles;
using Craftify.Application.Profile.Queries.GetAllWorkers;
using Craftify.Application.Profile.Queries.GetProfile;
using Craftify.Application.Profile.Queries.GetWorker;
using Craftify.Contracts.Profile;
using Craftify.Domain.Constants;
using Craftify.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Craftify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController(
        ISender _mediator,
        IMapper _mapper
        ) : ApiController
    {


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetProfileQuery (id));

            return result.Match(
                result => Ok(_mapper.Map<ProfileResult>(result)),
                                error => Problem(error));
        }


        [HttpGet("worker/{id}")]
        public async Task<IActionResult> GetWorker(Guid id)
        {
            var result = await _mediator.Send(new GetWorkerQuery(id));

            return result.Match(
                result => Ok(_mapper.Map<WorkerResult>(result)),Problem);
                                
        }

        [Authorize(Roles =AppConstants.Role_Admin)]
        [HttpGet("custormers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [Authorize(Roles = AppConstants.Role_Admin)]
        [HttpGet("workers")]
        public async Task<IActionResult> GetAllWorkers()
        {
            var result = await _mediator.Send(new GetAllWorkersQuery());

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet("payment/init")]
        public async Task<IActionResult> InitSubscribe([FromQuery] Guid userId, [FromQuery] Guid planId)
        {
            try
            {
                var orderId = await _mediator.Send(new InitSubscribeProfileCommand ( userId, planId ));
                return Ok(new { orderId = orderId.Value });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe(SubscriptionRequest request)
        {
            var commant = _mapper.Map<SubscribeProfileCommand>(request);
            var result = await _mediator.Send(commant);

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpPatch("accessChange/{Id}")]
        public async Task<IActionResult> AccessChange(Guid Id)
        {
            var commant = new AccessChangeProfileCommand(Id);
            var result = await _mediator.Send(commant);

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpPatch("worker/approvalChange/{Id}")]
        public async Task<IActionResult> ApprovalChange(Guid Id)
        {
            var commant = new ApprovalChangeProfileCommand(Id);
            var result = await _mediator.Send(commant);

            if (result.IsError)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id,ProfileRequest model)
        {
            var result = await _mediator.Send(new UpdateProfileCommand(Id, _mapper.Map<User>(model)));

            return result.Match(
                success => Ok(),
                error => Problem(error)
            );
        }

        [HttpPut("worker/{Id}")]
        public async Task<IActionResult> UpdateWorker(Guid Id, WorkerRequest model)
        {
            var result = await _mediator.Send(new UpdateWorkerCommand(Id, _mapper.Map<Worker>(model)));

            return result.Match(
                success => Ok(),Problem
            );
        }

        [HttpPut("picture/{id}")]
        public async Task<IActionResult> UploadProfilePicture(Guid id, IFormFile file)
        {
            var result = await _mediator.Send(new UploadProfilePictureCommand(id, file));

            return result.Match(
                success => Ok(result.Value),
                error => Problem(error)
            );
        }

        [HttpPut("worker/upload/doc")]
        public async Task<IActionResult> UploadWorkerDoc(IFormFile file)
        {
            var result = await _mediator.Send(new UploadWorkerDocCommand(file));

            return result.Match(
                success => Ok(new{docUrl = result.Value}),
                Problem
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteProfileCommand(id));

            return result.Match(
                success => Ok(),
                error => Problem(error)
            );
        }



    }
}
