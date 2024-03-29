﻿namespace SystemyWP.API.Logistics.Middleware
{
    public static class ServiceStatusMiddlewareExtensions
    {
        public static IApplicationBuilder UseServiceStatus(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ServiceStatusMiddleware>();
        }
    }
    
    public class ServiceStatusMiddleware
    {
        private readonly RequestDelegate _next;

        public ServiceStatusMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("STATUS", "OK");
            await _next(context);
        }
    }
}