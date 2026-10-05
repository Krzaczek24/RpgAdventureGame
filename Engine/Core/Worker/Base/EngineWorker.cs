using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RpgAdventureGame.Backend.Common;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Worker;
using RpgAdventureGame.Backend.Engine.Core.Worker.Interfaces;

namespace RpgAdventureGame.Backend.Engine.Core.Worker.Base
{
    internal abstract class EngineWorker<TJob>(
        ILogger logger,
        IMemoryCache cache,
        IServiceScopeFactory scopeFactory) : BackgroundService
        where TJob : IJob
    {
        protected Type WorkerType { get; } = typeof(TJob);

        protected ILogger Logger { get; } = logger;
        protected IMemoryCache Cache { get; } = cache;

        protected sealed override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.Info("Worker starting ...");

            DbWorker config;

            await using (var scope = scopeFactory.CreateAsyncScope())
            {
                var access = scope.ServiceProvider.GetRequiredService<IDbWorkerAccess>();

                config = await LoadConfigAsync(scope, stoppingToken)
                    ?? throw new InvalidOperationException($"Worker '{WorkerType.Name}' configuration not found");

                Logger.Info("Worker configuration loaded");
            }

            try
            {
                await RunAsync(config, scopeFactory, stoppingToken);
            }
            catch (OperationCanceledException ex)
            {
                Logger.Error(ex, "Worker has been stopped");
            }
        }

        protected abstract Task RunAsync(DbWorker config, IServiceScopeFactory scopeFactory, CancellationToken stoppingToken);

        protected Task<DbWorker> LoadConfigAsync(AsyncServiceScope scope, CancellationToken stoppingToken)
        {
            var config = Cache.GetOrCreateAsync($"{WorkerType.Name}_WORKER_CONFIG", opts =>
            {
                opts.AbsoluteExpirationRelativeToNow = EnvInfo.IsDebug ? TimeSpan.FromSeconds(10) : TimeSpan.FromMinutes(10);

                var access = scope.ServiceProvider.GetRequiredService<IDbWorkerAccess>();
                return access.GetWorkerAsync(WorkerType.Name, stoppingToken)!;
            });

            return config!;
        }
    }
}
