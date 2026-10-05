namespace RpgAdventureGame.Backend.Engine.Core
{
    public interface IJobContext
    {
        string JobName { get; }
        Guid InstanceId { get; }
        bool DidSomeJob { get; set; }
    }

    internal class JobContext : IJobContext
    {
        public required string JobName { get; init; }
        public required Guid InstanceId { get; init; }
        public bool DidSomeJob { get; set; }
    }
}
