using RpgAdventureGame.Backend.Database.SQLite.Entities.EngineWorkerJob;

namespace RpgAdventureGame.Backend.Engine.Core.Waiter.Base
{
    internal interface IWaiter
    {
        DateTime NextTick { get; }
        TimeSpan Delay { get; }
        Task AwaitAsync(CancellationToken cancellationToken);
        void Update(DbEngineWorkerJob config);
    }
}
