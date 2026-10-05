using RpgAdventureGame.Backend.Engine.Core;
using RpgAdventureGame.Backend.Engine.Core.Worker.Interfaces;

namespace RpgAdventureGame.Backend.Engine.Jobs
{
    internal class TravelProcessingJob : IJob
    {
        public Task ExecuteAsync(IJobContext context, CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
