using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MasterService.Middlewares
{
    public static class CustomResponseHeadersMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomResponseHeaders(this IApplicationBuilder builder) => builder.UseMiddleware<CustomResponseHeadersMiddleware>();
    }

    public class CustomResponseHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomResponseHeadersMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("API-RES", "Master-Server-Response");
            context.Response.Headers.Add("X-Developed-By", "JW");
            await _next(context);
        }
    }
}