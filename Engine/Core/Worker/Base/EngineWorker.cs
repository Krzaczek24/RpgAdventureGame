using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RpgAdventureGame.Backend.Common;
using RpgAdventureGame.Backend.Database.SQLite.Entities.EngineWorkerJob;
using RpgAdventureGame.Backend.Engine.Core.Worker.Interfaces;

namespace RpgAdventureGame.Backend.Engine.Core.Worker.Base
{
    internal abstract class EngineWorker<TJob>(
        ILogger logger,
        IMemoryCache cache,
        IServiceScopeFactory scopeFactory) : BackgroundService
        where TJob : IJob
    {
        protected static string JobName { get; } = typeof(TJob).Name;

        protected ILogger Logger { get; } = logger;
        protected IMemoryCache Cache { get; } = cache;

        protected sealed override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.Info("Worker is preparing to start job ...");

            DbEngineWorkerJob config;

            await using (var scope = scopeFactory.CreateAsyncScope())
            {
                var access = scope.ServiceProvider.GetRequiredService<IDbWorkerAccess>();

                config = await LoadConfigAsync(scope, stoppingToken)
                    ?? throw new InvalidOperationException($"Worker's job '{JobName}' configuration has been not found");
            }

            try
            {
                Logger.Info("Worker is starting job ...");
                await RunAsync(config, scopeFactory, stoppingToken);
            }
            catch (OperationCanceledException ex)
            {
                Logger.Error(ex, "Worker has been stopped");
            }
        }

        protected abstract Task RunAsync(DbEngineWorkerJob config, IServiceScopeFactory scopeFactory, CancellationToken stoppingToken);

        protected Task<DbEngineWorkerJob> LoadConfigAsync(AsyncServiceScope scope, CancellationToken stoppingToken)
        {
            var job = Cache.GetOrCreateAsync($"{JobName}_JOB_CONFIG", opts =>
            {
                opts.AbsoluteExpirationRelativeToNow = EnvInfo.IsDebug ? TimeSpan.FromSeconds(10) : TimeSpan.FromMinutes(10);

                Logger.Info("Loading job configuration from database ...");

                var access = scope.ServiceProvider.GetRequiredService<IDbWorkerAccess>();
                return access.GetJobAsync(JobName, stoppingToken)!;
            });

            Logger.Info("Job configuration loaded");
            return job!;
        }
    }
}
