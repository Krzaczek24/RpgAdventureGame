using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Worker;
using RpgAdventureGame.Backend.Engine.Core.Worker.Base;
using RpgAdventureGame.Backend.Engine.Core.Worker.Interfaces;

namespace RpgAdventureGame.Backend.Engine.Core.Worker.Implementations
{
    internal sealed class ContinuousWorker<TJob>(ILogger logger, IMemoryCache cache, IServiceScopeFactory scopeFactory)
        : EngineWorker<TJob>(logger, cache, scopeFactory)
        where TJob : IJob
    {
        protected sealed override async Task RunAsync(DbWorker config, IServiceScopeFactory scopeFactory, CancellationToken stoppingToken)
        {
            if (config.IdleInterval is null)
                throw new InvalidOperationException($"Job '{typeof(TJob).Name}' executing by '{nameof(ContinuousWorker<>)}', requires filled up '{nameof(config.IdleInterval)}' parameter");

            var timer = new PeriodicTimer(config.IdleInterval.Value);

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await using var scope = scopeFactory.CreateAsyncScope();
                var worker = ActivatorUtilities.CreateInstance<TJob>(scope.ServiceProvider);
                await worker.ExecuteAsync(stoppingToken);
            }
        }
    }
}
