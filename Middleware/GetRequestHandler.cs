namespace Middleware_Assignment.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GetRequestHandler
    {
        private readonly RequestDelegate _next;

        public GetRequestHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 200;
            await httpContext.Response.WriteAsync("No Response");

            //return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HandleGetRequestsExtensions
    {
        public static IApplicationBuilder UseGetRequestHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GetRequestHandler>();
        }
    }
}
