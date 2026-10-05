using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using RpgAdventureGame.Backend.Database.SQLite.Entities.EngineWorkerJob;
using RpgAdventureGame.Backend.Engine.Core.Worker.Base;
using RpgAdventureGame.Backend.Engine.Core.Worker.Interfaces;

namespace RpgAdventureGame.Backend.Engine.Core.Worker.Implementations
{
    internal sealed class ContinuousWorker<TJob>(ILogger logger, IMemoryCache cache, IServiceScopeFactory scopeFactory)
        : EngineWorker<TJob>(logger, cache, scopeFactory)
        where TJob : IJob
    {
        protected sealed override async Task RunAsync(DbEngineWorkerJob config, IServiceScopeFactory scopeFactory, CancellationToken stoppingToken)
        {
            if (config.IdleInterval is null)
                throw new InvalidOperationException($"Job '{JobName}' executing by '{nameof(ContinuousWorker<>)}', requires filled up '{nameof(config.IdleInterval)}' parameter");

            var timer = new PeriodicTimer(config.IdleInterval.Value);

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                Logger.Info("Starting job ...");
                while (false)
                {
                    // to do no delay loop if DoNextJob
                    Logger.Info("Starting next job ...");
                }
                await using var scope = scopeFactory.CreateAsyncScope();
                var context = new JobContext
                {
                    JobName = JobName,
                    InstanceId = Guid.NewGuid().ToString(),
                    WorkerType = WorkerType.Continuous,
                };
                var job = ActivatorUtilities.CreateInstance<TJob>(scope.ServiceProvider);
                await job.ExecuteAsync(context, stoppingToken);
                Logger.Info("Job is done");
            }
        }
    }
}
