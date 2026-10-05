using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NCrontab;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Worker;
using RpgAdventureGame.Backend.Engine.Core.Worker.Base;
using RpgAdventureGame.Backend.Engine.Core.Worker.Interfaces;

namespace RpgAdventureGame.Backend.Engine.Core.Worker.Implementations
{
    internal sealed class ScheduleWorker<TJob>(ILogger logger, IMemoryCache cache, IServiceScopeFactory scopeFactory)
        : EngineWorker<TJob>(logger, cache, scopeFactory)
        where TJob : IJob
    {
        protected sealed override async Task RunAsync(DbWorker config, IServiceScopeFactory scopeFactory, CancellationToken stoppingToken)
        {
            if (config.NextRunTimestamp is null)
            {
                throw new NotImplementedException("ToDo");
            }

            if (config.CronExpression is null)
                throw new InvalidOperationException($"Job '{typeof(TJob).Name}' executing by '{nameof(ScheduleWorker<>)}' type, requires filled up '{nameof(config.CronExpression)}' parameter");

            bool includeSeconds = config.CronExpression.Split(' ').Length > 5;
            var cron = CrontabSchedule.TryParse(config.CronExpression, new(){ IncludingSeconds = includeSeconds })
                ?? throw new InvalidOperationException($"Job '{typeof(TJob).Name}' has invalid '{nameof(config.CronExpression)}' parameter");

            throw new NotImplementedException("ToDo");
        }
    }
}
