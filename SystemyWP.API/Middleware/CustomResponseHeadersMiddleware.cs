using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace SystemyWP.API.Middleware
{
    public static class CustomResponseHeadersMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomResponseHeaders(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomResponseHeadersMiddleware>();
        }
    }
    
    public class CustomResponseHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomResponseHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("SWP-RES", "SystemyWP-Server-Response");
            context.Response.Headers.Add("X-Developed-By", "systemy-wspomagania-pracy");
            await _next(context);
        }
    }
}