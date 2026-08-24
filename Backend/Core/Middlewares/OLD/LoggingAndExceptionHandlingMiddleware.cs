using Krzaq.Converters.EnumToString;
using Krzaq.Exceptions.Http.Base;
using Krzaq.Exceptions.Http.Error.Base;
using NLog;
using RpgAdventureGame.Backend.Core.Errors;
using RpgAdventureGame.Backend.Core.Extensions;
using System.Text;
using System.Text.Json;

namespace RpgAdventureGame.Backend.Core.Middlewares.OLD
{
    public class LoggingAndExceptionHandlingMiddleware(RequestDelegate next)
    {
        protected static NLog.ILogger Logger { get; } = LogManager.GetLogger(nameof(LoggingMiddleware));
        protected static JsonSerializerOptions JsonOpts { get; } = new()
        {
            Converters = { new EnumToStringConverter<ErrorCode>() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public async Task Invoke(HttpContext httpContext)
        {
            await HandleRequest(httpContext);
            await HandleResponse(httpContext);
        }

        protected virtual async Task HandleRequest(HttpContext httpContext)
        {
            httpContext.PassRequestId();
            string bodyText = string.Empty;
            httpContext.Request.EnableBuffering();
            using (StreamReader reader = new(httpContext.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, 1024, leaveOpen: true))
            {
                bodyText = await reader.ReadToEndAsync();
                httpContext.Request.Body.Position = 0L;
            }

            LogRequest(httpContext, bodyText);
        }

        protected virtual async Task HandleResponse(HttpContext httpContext)
        {
            var originalBodyStream = httpContext.Response.Body;
            var responseBody = new MemoryStream();
            httpContext.Response.Body = responseBody;
            try
            {
                await next.Invoke(httpContext);
            }
            catch (HttpException ex)
            {
                httpContext.Response.StatusCode = (int)ex.StatusCode;
                await HandleException(httpContext, ex);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = 500;
                await HandleException(httpContext, ex);
            }

            httpContext.Response.Body.Seek(0L, SeekOrigin.Begin);
            string bodyText = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();
            httpContext.Response.Body.Seek(0L, SeekOrigin.Begin);
            LogResponse(httpContext, bodyText);
            await responseBody.CopyToAsync(originalBodyStream);
        }

        protected virtual async Task HandleException(HttpContext httpContext, Exception exception)
        {
            LogException(httpContext, exception);
            httpContext.Response.ContentType = "application/json";
            var errorResponse = GetErrorResponse(exception);
            string responseBody = JsonSerializer.Serialize(errorResponse, JsonOpts);
            await httpContext.Response.WriteAsync(responseBody);
        }

        protected virtual void LogRequest(HttpContext httpContext, string bodyText)
        {
            Logger.Info($"REQUEST  ({httpContext.GetRequestId()}) | PATH ({httpContext.Request.GetPath()}) | BODY ({bodyText})");
        }

        protected virtual void LogResponse(HttpContext httpContext, string bodyText)
        {
            Logger.Info($"RESPONSE ({httpContext.GetRequestId()}) | CODE ({httpContext.Response.StatusCode}) | BODY ({bodyText})");
        }

        protected virtual void LogException(HttpContext httpContext, Exception exception)
        {
            if (httpContext.Response.StatusCode >= 500)
            {
                Logger.Error(exception, exception.Message);
            }
        }

        protected virtual ErrorResponse GetErrorResponse(Exception exception) => exception switch
        {
            HttpErrorException<ErrorModel> ex => new ErrorResponse(ex.Errors),
            _ => new ErrorResponse([new(ErrorCode.Unknown, exception.Message)])
        };
    }

    public static class LoggingAndExceptionHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseLoggingAndExceptionHandlingMiddleware(this IApplicationBuilder app) => app.UseMiddleware<LoggingAndExceptionHandlingMiddleware>();
    }
}
