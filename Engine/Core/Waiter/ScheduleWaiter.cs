using NCrontab;
using RpgAdventureGame.Backend.Database.SQLite.Entities.EngineWorkerJob;
using RpgAdventureGame.Backend.Engine.Core.Waiter.Base;

namespace RpgAdventureGame.Backend.Engine.Core.Waiter
{
    internal class ScheduleWaiter : IWaiter
    {
        private CrontabSchedule Cron { get; set; } = CrontabSchedule.Parse("* * * * * *", new() { IncludingSeconds = true });

        public DateTime NextTick { get; private set; } = DateTime.Now.AddSeconds(1);
        public TimeSpan Delay { get; private set; } = TimeSpan.FromSeconds(1);

        public async Task AwaitAsync(CancellationToken cancellationToken)
        {
            Delay = NextTick - DateTime.Now;
            if (Delay <= TimeSpan.Zero) Delay = TimeSpan.FromMilliseconds(100);

            await Task.Delay(Delay, cancellationToken);
            NextTick = Cron.GetNextOccurrence(DateTime.Now);
        }

        public void Update(DbEngineWorkerJob config)
        {
            if (config.CronExpression is null)
                throw new InvalidOperationException("Cron expression cannot be null");

            bool includeSeconds = config.CronExpression.Split(' ').Length > 5;

            Cron = CrontabSchedule.TryParse(config.CronExpression, new() { IncludingSeconds = includeSeconds })
                ?? throw new InvalidOperationException("Invalid cron expression");
        }
    }
}
