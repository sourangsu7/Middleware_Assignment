using Middleware_Assignment.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapWhen((context) => { return context.Request.Method == HttpMethods.Post && context.Request.Path == "/"; },
appBuilder =>
{
    appBuilder.UsePostRequestHandler();

    #region moved to custom middleware extension
    //appBuilder.Run(async (ctx) =>
    //{
    //    var serverRequest = new StreamReader(ctx.Request.Body);
    //    var serverRequestDecodedData = await serverRequest.ReadToEndAsync();

    //    var requestParts = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(serverRequestDecodedData);

    //    requestParts.TryGetValue("email", out var _email);
    //    requestParts.TryGetValue("password", out var _password);

    //    var email = _email != StringValues.Empty ? _email.ToString() : string.Empty;
    //    var password = _password != StringValues.Empty ? _password.ToString() : string.Empty;


    //    if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
    //    {
    //        ctx.Response.StatusCode = 400;
    //        await ctx.Response.WriteAsync("Invalid input for email \n");
    //        await ctx.Response.WriteAsync("Invalid input for password \n");
    //    }
    //    else if (string.IsNullOrEmpty(password))
    //    {
    //        ctx.Response.StatusCode = 400;
    //        await ctx.Response.WriteAsync("Invalid input for 'password' \n");
    //    }
    //    else if (string.IsNullOrEmpty(email))
    //    {
    //        ctx.Response.StatusCode = 400;
    //        await ctx.Response.WriteAsync("Invalid input for 'email' \n");
    //    }
    //    else
    //    {
    //        ctx.Response.StatusCode = 200;
    //        await ctx.Response.WriteAsync("Successful login \n");
    //    }

    //});
    #endregion
});

app.MapWhen((context) => { return context.Request.Method == HttpMethods.Get && context.Request.Path == "/"; },
    appBuilder =>
    {
        app.UseGetRequestHandler();

        #region moved to custom middleware extension

        //app.Run(async (context) =>
        //{
        //    context.Response.StatusCode = 200;
        //    await context.Response.WriteAsync("No Response");
        //}); 
        #endregion
    });
app.Run();
