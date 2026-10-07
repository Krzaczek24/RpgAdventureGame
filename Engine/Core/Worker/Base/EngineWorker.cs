using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RpgAdventureGame.Backend.Common;
using RpgAdventureGame.Backend.Database.SQLite.Entities.EngineWorkerJob;
using RpgAdventureGame.Backend.Engine.Core.Job;
using RpgAdventureGame.Backend.Engine.Core.Waiter.Interface;

namespace RpgAdventureGame.Backend.Engine.Core.Worker.Base
{
    internal abstract class EngineWorker<TJob, TWaiter>(
        ILogger logger,
        IMemoryCache cache,
        IServiceScopeFactory scopeFactory,
        TWaiter waiter) : BackgroundService
        where TJob : IJob
        where TWaiter : IWaiter
    {
        private readonly static Lazy<bool> isAlreadyAssigned = new(() => true);
        protected static string JobName { get; } = typeof(TJob).Name;
        protected static string WaiterTypeName { get; } = typeof(TWaiter).Name;

        protected ILogger Logger { get; } = logger;
        protected IMemoryCache Cache { get; } = cache;

        protected sealed override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (IsAlreadyRunning())
            {
                Logger.Warn($"'{JobName}' is already being performed by another worker");
                return;
            }

            Logger.Info($"Worker is preparing to start '{JobName}' ...");

            try
            {
                var config = await LoadConfigAsync(stoppingToken) ?? throw new InvalidOperationException($"'{JobName}' configuration has been not found, worker abandons job");

                UpdateWaiterTick(waiter, config);
                await waiter.AwaitAsync(stoppingToken);

                Logger.Info($"Worker's next '{JobName}' execution scheduled for: {waiter.NextTick:yyyy-MM-dd HH:mm} (in {waiter.Delay})");

                using (var scope = scopeFactory.CreateAsyncScope())
                {
                    var access = scope.ServiceProvider.GetRequiredService<DbEngineWorkerJob>();

                    // update params
                }

                await waiter.AwaitAsync(stoppingToken);

                while (true)
                {
                    config = await LoadConfigAsync(stoppingToken);
                    UpdateWaiterTick(waiter, config);

                    if (config.Active)
                    {
                        try
                        {
                            await ExecuteJobAsync(stoppingToken);
                        }
                        catch (Exception ex)
                        {
                            config.ErrorCounter++;
                            // add log + increment errors
                        }
                    }
                    else
                    {
                        Logger.Info($"'{JobName}' is disabled, skipping execution, worker goes back to sleep");
                    }

                    using (var scope = scopeFactory.CreateAsyncScope())
                    {
                        // update params
                    }

                    await waiter.AwaitAsync(stoppingToken);
                }
            }
            catch (OperationCanceledException ex)
            {
                Logger.Error(ex, $"Worker performing '{JobName}' has been stopped");
            }
        }

        protected abstract void UpdateWaiterTick(TWaiter waiter, DbEngineWorkerJob config);

        protected virtual JobContext CreateContext() => new()
        {
            JobName = JobName,
            InstanceId = Guid.NewGuid().ToString(),
        };

        private async Task ExecuteJobAsync(CancellationToken stoppingToken)
        {
            Logger.Info($"Starting '{JobName}' ...");

            var context = CreateContext();
            do
            {
                context.Iteration++;

                await using var scope = scopeFactory.CreateAsyncScope();
                var job = ActivatorUtilities.CreateInstance<TJob>(scope.ServiceProvider);
                await job.ExecuteAsync(context, stoppingToken);
            }
            while (context.DoNextJob);

            Logger.Info($"Worker finished '{JobName}'");
        }

        protected Task<DbEngineWorkerJob> LoadConfigAsync(CancellationToken stoppingToken)
        {
            var job = Cache.GetOrCreateAsync($"{JobName}_JOB_CONFIG", opts =>
            {
                opts.AbsoluteExpirationRelativeToNow = EnvInfo.IsDebug ? TimeSpan.FromSeconds(10) : TimeSpan.FromMinutes(10);

                Logger.Info($"Loading '{JobName}' configuration from database ...");

                using var scope = scopeFactory.CreateAsyncScope();
                var access = scope.ServiceProvider.GetRequiredService<IDbWorkerAccess>();
                return access.GetJobAsync(JobName, stoppingToken)!;
            });

            Logger.Info($"'{JobName}' configuration loaded");
            return job!;
        }

        private static bool IsAlreadyRunning()
        {
            if (isAlreadyAssigned.IsValueCreated)
                return true;

            lock (isAlreadyAssigned)
                return isAlreadyAssigned.IsValueCreated || isAlreadyAssigned.Value;
        }
    }
}
