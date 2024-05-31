using Craftify.Application.Profile.Commands.DeleteProfile;
using Craftify.Application.Profile.Commands.UpdateProfile;
using Craftify.Application.Profile.Commands.UploadProfilePicture;
using Craftify.Application.Profile.Common;
using Craftify.Application.Profile.Queries.GetProfile;
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


        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(Guid Id,ProfileRequest model)
        {
            var result = await _mediator.Send(new UpdateProfileCommand(Id, _mapper.Map<User>(model)));

            return result.Match(
                success => Ok(),
                error => Problem(error)
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
