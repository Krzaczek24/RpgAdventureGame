using RpgAdventureGame.WebApi.Core.Extensions;

namespace RpgAdventureGame.WebApi.Core.Middlewares
{
    public class CopyingHeadersMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.PassRequestId();
            await next.Invoke(httpContext);
        }
    }

    public static class CopyingHeadersMiddlewareExtension
    {
        public static IApplicationBuilder UseCopyingHeadersMiddleware(this IApplicationBuilder app) => app.UseMiddleware<CopyingHeadersMiddleware>();
    }
}
