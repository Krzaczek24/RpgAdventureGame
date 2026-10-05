using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NCrontab;
using RpgAdventureGame.Backend.Database.SQLite.Entities.EngineWorkerJob;
using RpgAdventureGame.Backend.Engine.Core.Worker.Base;
using RpgAdventureGame.Backend.Engine.Core.Worker.Interfaces;

namespace RpgAdventureGame.Backend.Engine.Core.Worker.Implementations
{
    internal sealed class ScheduleWorker<TJob>(ILogger logger, IMemoryCache cache, IServiceScopeFactory scopeFactory)
        : EngineWorker<TJob>(logger, cache, scopeFactory)
        where TJob : IJob
    {
        protected sealed override async Task RunAsync(DbEngineWorkerJob config, IServiceScopeFactory scopeFactory, CancellationToken stoppingToken)
        {
            if (config.CronExpression is null)
                throw new InvalidOperationException($"Job '{typeof(TJob).Name}' executing by '{nameof(ScheduleWorker<>)}' type, requires filled up '{nameof(config.CronExpression)}' parameter");

            bool includeSeconds = config.CronExpression.Split(' ').Length > 5;
            var cron = CrontabSchedule.TryParse(config.CronExpression, new(){ IncludingSeconds = includeSeconds })
                ?? throw new InvalidOperationException($"Job '{typeof(TJob).Name}' has invalid '{nameof(config.CronExpression)}' parameter");

            config.NextRunTimestamp = cron.GetNextOccurrence(DateTime.Now);

            TimeSpan delay = config.NextRunTimestamp.Value - DateTime.Now;
            if (delay <= TimeSpan.Zero) delay = TimeSpan.FromMilliseconds(100);

            Logger.Info("Next job execution scheduled for: {0:yyyy-MM-dd HH:mm} (in {1})", config.NextRunTimestamp, delay);

            await Task.Delay(delay, stoppingToken);

            Logger.Info("Starting job ...");
            await using var scope = scopeFactory.CreateAsyncScope();
            var context = new JobContext
            {
                JobName = JobName,
                InstanceId = Guid.NewGuid().ToString(),
                WorkerType = WorkerType.Schedule,
            };
            var job = ActivatorUtilities.CreateInstance<TJob>(scope.ServiceProvider);
            await job.ExecuteAsync(context, stoppingToken);
            Logger.Info("Job is done");
        }
    }
}
