using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace TechnologyApplication
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Middleware
    {
        private readonly RequestDelegate _next;
    
        private readonly ILogger logger;

        
        public Middleware(RequestDelegate next,ILoggerFactory loggerFactory)
        {
            _next = next;
            logger = loggerFactory.CreateLogger("Middleware");

        }

        public async Task Invoke(HttpContext httpContext)
        {
            logger.LogInformation("Middleware executing..");
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
    public class DefaultFilesExtensions
    {
    }
}
