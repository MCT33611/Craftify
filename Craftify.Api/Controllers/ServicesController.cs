using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Craftify.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController() : ControllerBase
    {
        [HttpGet]
        public IActionResult ListServices()
        {
            return Ok(Array.Empty<string>());
        }
    }
}
