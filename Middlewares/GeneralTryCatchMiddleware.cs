
namespace Middlewares;

public class GeneralTryCatchMiddleware
{
    private readonly RequestDelegate Next;
     private readonly ILogger<GeneralTryCatchMiddleware> Logger;

    public GeneralTryCatchMiddleware (RequestDelegate next, ILogger<GeneralTryCatchMiddleware> logger)
    {
        Next = next;
        Logger = logger;
    }

    public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (System.Exception e)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"message\": \"Internal Server Error\"}");

            Logger.LogError(e, "Middleware general error handler");
        }
    }
}

static class UseGeneralTryCatchMiddlewareExtencion
{
    public static IApplicationBuilder UseGeneralTryCatchMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GeneralTryCatchMiddleware>();
    }
}