using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using RpgAdventureGame.Backend.Engine.Core.Job;
using RpgAdventureGame.Backend.Engine.Core.Waiter;
using RpgAdventureGame.Backend.Engine.Core.Worker.Base;

namespace RpgAdventureGame.Backend.Engine.Core.Worker
{
    internal sealed class ScheduleWorker<TJob>(ILogger logger, IMemoryCache cache, IServiceScopeFactory scopeFactory)
        : EngineWorker<TJob, ScheduleWaiter>(logger, cache, scopeFactory) where TJob : IJob { }
}
