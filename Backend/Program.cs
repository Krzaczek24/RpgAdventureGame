using FluentValidation;
using Krzaq.MediatR;
using Krzaq.MediatR.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog.Extensions.Logging;
using NLog.Web;
using RpgAdventureGame.Backend.Core.Errors;
using RpgAdventureGame.Backend.Core.Middlewares;
using RpgAdventureGame.Database.SQLite;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RpgAdventureGame.Backend
{
    public class Program
    {
        private const string OPENAPI_PREFIX = "/openapi";
        private const string DOC_NAME = "swagger";
        public const string ENDPOINT = $"{OPENAPI_PREFIX}/{DOC_NAME}.json";

#if DEBUG
        public const bool IS_DEBUG = true;
#else
        public const bool IS_DEBUG = false;
#endif

        public static void Main(string[] args)
        {
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("config.json", optional: false);

            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(LogLevel.Trace);

            builder.Host.UseNLog();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            builder.Services.AddOpenApi(DOC_NAME);

            builder.Services.ConfigureHttpJsonOptions(opts =>
            {
                opts.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            builder.Services.AddHttpContextAccessor();

            //builder.Services.Configure<DatabaseConfig>(builder.Configuration.GetSection("database:mikrus"));

            builder.Services.AddAppDatabase(IS_DEBUG ? new LoggerFactory([new NLogLoggerProvider()]) : null);

            builder.Services.AddMediator().AddHandlers().AddValidators().AddSingleton<IRequestErrorsHandler, ErrorHandler>();

            builder.Services.Configure<ApiBehaviorOptions>(opts =>
            {
                opts.SuppressModelStateInvalidFilter = true;
                opts.SuppressConsumesConstraintForFormFileParameters = true;
            });

            var app = builder.Build();

            app.MapOpenApi();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerUI(x => x.SwaggerEndpoint(ENDPOINT, nameof(RpgAdventureGame)));
            }

            app.UseWhen(
                context => !context.Request.Path.StartsWithSegments(OPENAPI_PREFIX),
                app => app
                    .UseCopyingHeadersMiddleware()
                    .UseLoggingMiddleware()
                    .UseExceptionHandlingMiddleware()
            );

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
