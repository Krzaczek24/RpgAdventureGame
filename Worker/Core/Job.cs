namespace RpgAdventureGame.Backend.Worker.Core
{
    internal abstract class Job() : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        protected abstract Task Process(CancellationToken stoppingToken);
    }
}
