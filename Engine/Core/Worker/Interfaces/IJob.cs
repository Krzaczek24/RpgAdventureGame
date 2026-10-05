namespace RpgAdventureGame.Backend.Engine.Core.Worker.Interfaces
{
    public interface IJob
    {
        Task ExecuteAsync(IJobContext context, CancellationToken stoppingToken);
    }
}
