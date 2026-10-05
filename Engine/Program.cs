using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using RpgAdventureGame.Backend.Common;
using RpgAdventureGame.Backend.Engine.Core.Worker.Implementations;
using RpgAdventureGame.Backend.Engine.Workers;
using RpgAdventureGame.Database.SQLite;

namespace RpgAdventureGame.Backend.Engine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Logging.AddNLog();

            builder.Services.AddAppDatabase(EnvInfo.IsDebug ? new LoggerFactory([new NLogLoggerProvider()]) : null);

            builder.Services.AddHostedService<ContinuousWorker<TravelProcessingJob>>();

            var app = builder.Build();

            app.Run();
        }
    }
}
