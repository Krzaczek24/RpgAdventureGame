using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using RpgAdventureGame.Backend.Engine.Core.Job;
using RpgAdventureGame.Backend.Engine.Core.Waiter;
using RpgAdventureGame.Backend.Engine.Core.Worker.Base;

namespace RpgAdventureGame.Backend.Engine.Core.Worker
{
    internal sealed class ContinuousWorker<TJob>(ILogger logger, IMemoryCache cache, IServiceScopeFactory scopeFactory)
        : EngineWorker<TJob, IntervalWaiter>(logger, cache, scopeFactory) where TJob : IJob { }
}
