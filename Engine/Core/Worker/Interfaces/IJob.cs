namespace RpgAdventureGame.Backend.Engine.Core.Worker.Interfaces
{
    internal interface IJob
    {
        Task ExecuteAsync(CancellationToken stoppingToken);
    }
}
