using Craftify.Application.Profile.Commands.UpdateProfile;
using Craftify.Application.Profile.Common;
using Craftify.Application.Profile.Queries.GetProfile;
using Craftify.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Craftify.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProfileCommand model)
        {
            var result = await _mediator.Send(new UpdateProfileCommand(id, _mapper.Map<User>(model)));

            return result.Match(
                success => Ok(),
                error => Problem(error)
            );
        }



    }
}
