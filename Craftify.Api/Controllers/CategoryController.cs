using Craftify.Application.Category.Commands.CreateCategory;
using Craftify.Application.Category.Commands.DeleteCategory;
using Craftify.Application.Category.Commands.UpdateCategory;
using Craftify.Application.Category.Queries.GetAllCategory;
using Craftify.Application.Category.Queries.GetCategory;
using Craftify.Contracts.Category;
using Craftify.Domain.Constants;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Craftify.Api.Controllers
{
    //[Authorize(Roles = AppConstants.Role_Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(
        IMapper _mapper,
        IMediator _mediator
        ) : ApiController
    {


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _mediator.Send(new GetAllCategoryQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var query = _mapper.Map<GetCategoryQuery>(new { id });
            var result = _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryRequest request)
        {

            var command = _mapper.Map<CreateCategoryCommand>(request);
            var CategoryId = await _mediator.Send(command);
            return CreatedAtAction("GetCategory", new { id = CategoryId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryRequest request)
        {
            TypeAdapterConfig<CategoryRequest, UpdateCategoryCommand>.NewConfig().
                Map(dest => dest.Id, src => id);

            var command = _mapper.Map<UpdateCategoryCommand>(request);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _mediator.Send(new DeleteCategoryCommand(id));
            return NoContent();
        }
    }
}
