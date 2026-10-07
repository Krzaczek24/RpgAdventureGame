using Microsoft.EntityFrameworkCore;
using RpgAdventureGame.Backend.Database.SQLite.Entities.EngineWorkerJobEvent;

namespace RpgAdventureGame.Backend.Database.SQLite.Entities.EngineWorkerJob
{
    public interface IDbWorkerAccess
    {
        Task<DbEngineWorkerJob?> GetJobAsync(string name, CancellationToken cancellationToken = default);
        Task LogEventAsync(int jobId, WorkerJobEventType type, string message, CancellationToken cancellationToken = default);
    }

    internal class DbEngineWorkerJobAccess(Db db) : IDbWorkerAccess
    {
        public Task<DbEngineWorkerJob?> GetJobAsync(string name, CancellationToken cancellationToken = default)
            => db.Jobs.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        public async Task LogEventAsync(int jobId, WorkerJobEventType type, string message, CancellationToken cancellationToken = default)
        {
            await db.JobEvents.AddAsync(new()
            {
                JobId = jobId,
                Type = type,
                Timestamp = DateTime.Now,
                Message = message,
            }, cancellationToken);
        }
    }
}
