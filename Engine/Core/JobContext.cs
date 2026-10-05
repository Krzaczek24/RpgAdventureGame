using RpgAdventureGame.Backend.Database.SQLite.Entities.EngineWorkerJob;

namespace RpgAdventureGame.Backend.Engine.Core
{
    public interface IJobContext
    {
        string JobName { get; }
        string InstanceId { get; }
        bool DoNextJob { get; set; }
        WorkerType WorkerType { get; }
    }

    internal class JobContext : IJobContext
    {
        public required string JobName { get; init; }
        public required string InstanceId { get; init; }
        public bool DoNextJob { get; set; }
        public required WorkerType WorkerType { get; init; }
    }
}
