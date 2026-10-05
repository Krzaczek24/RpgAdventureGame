using RpgAdventureGame.Backend.Database.SQLite.Base;

namespace RpgAdventureGame.Backend.Database.SQLite.Entities.EngineWorkerJob
{
    public class DbEngineWorkerJob : DbTable
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime? LastRunTimestamp { get; set; }
        public DateTime? LastSuccessfullRunTimestamp { get; set; }
        public DateTime? NextRunTimestamp { get; set; }
        public int ErrorCounter { get; set; }
        public WorkerType Type { get; set; }
        public string? CronExpression { get; set; }
        public TimeSpan? IdleInterval { get; set; }
    }

    public enum WorkerType
    {
        Continuous,
        Schedule,
    }
}
