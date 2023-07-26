using Microsoft.Extensions.Primitives;

namespace Middleware_Assignment.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class PostRequestHandler
    {
        private readonly RequestDelegate _next;

        public PostRequestHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var serverRequest = new StreamReader(httpContext.Request.Body);
            var serverRequestDecodedData = await serverRequest.ReadToEndAsync();

            var requestParts = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(serverRequestDecodedData);

            requestParts.TryGetValue("email", out var _email);
            requestParts.TryGetValue("password", out var _password);

            var email = _email != StringValues.Empty ? _email.ToString() : string.Empty;
            var password = _password != StringValues.Empty ? _password.ToString() : string.Empty;


            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Invalid input for email \n");
                await httpContext.Response.WriteAsync("Invalid input for password \n");
            }
            else if (string.IsNullOrEmpty(password))
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Invalid input for 'password' \n");
            }
            else if (string.IsNullOrEmpty(email))
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Invalid input for 'email' \n");
            }
            else
            {
                httpContext.Response.StatusCode = 200;
                await httpContext.Response.WriteAsync("Successful login \n");
            }

            //return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class PostRequestHandlerExtensions
    {
        public static IApplicationBuilder UsePostRequestHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PostRequestHandler>();
        }
    }
}
