using Azure.Identity;
using Craftify.Application.Common.Interfaces.Authentication;
using Craftify.Application.Common.Interfaces.Persistence.IRepository;
using Craftify.Contracts.Authentication;
using Craftify.Infrastructure.Authentication;
using Craftify.Infrastructure.Presistence;
using ErrorOr;
using Google.Apis.Auth.OAuth2.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace Craftify.Api.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        


        protected IActionResult Problem(List<Error> errors)
        {
            if (errors.Count is 0)
                return Problem();
            if (errors.All(error => error.Type == ErrorType.Validation))
            {
                return ValidationProblem(errors);
            }

            return Problem(errors[0]);
        }

#pragma warning disable CA1859 
        private IActionResult Problem(Error error)
#pragma warning restore CA1859 
        {
            var statusCode = error.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
            return Problem(statusCode: statusCode, title: error.Description);
        }

#pragma warning disable CA1859 
        private IActionResult ValidationProblem(List<Error> errors)
#pragma warning restore CA1859 
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
            }
            return ValidationProblem(modelStateDictionary);
        }



    }
}
