using Newtonsoft.Json;
using System.Net;

namespace Craftify.Api.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
            {
                var code = HttpStatusCode.InternalServerError; // 500 if unexpected
                var result = JsonConvert.SerializeObject(new { error = "An error occurred while processing your request" });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                return context.Response.WriteAsync(result);
            }
        }

}
