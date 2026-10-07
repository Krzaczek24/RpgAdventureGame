using RpgAdventureGame.Backend.Database.SQLite.Entities.EngineWorkerJob;
using RpgAdventureGame.Backend.Engine.Core.Waiter.Base;

namespace RpgAdventureGame.Backend.Engine.Core.Waiter
{
    internal class IntervalWaiter : IWaiter
    {
        private PeriodicTimer Timer { get; } = new(TimeSpan.FromSeconds(1));

        public DateTime NextTick { get; private set; } = DateTime.Now.Add(TimeSpan.FromSeconds(1));
        public TimeSpan Delay => Timer.Period;

        public async Task AwaitAsync(CancellationToken cancellationToken)
        {
            await Timer.WaitForNextTickAsync(cancellationToken);
            NextTick = DateTime.Now.Add(Timer.Period);
        }

        public void Update(DbEngineWorkerJob config)
        {
            Timer.Period = config.IdleInterval ?? throw new InvalidOperationException("Period value cannot be null");
        }
    }
}
