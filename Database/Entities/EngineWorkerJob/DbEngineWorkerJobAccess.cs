using Microsoft.EntityFrameworkCore;

namespace RpgAdventureGame.Backend.Database.SQLite.Entities.EngineWorkerJob
{
    public interface IDbWorkerAccess
    {
        Task<DbEngineWorkerJob?> GetJobAsync(string name, CancellationToken cancellationToken = default);
    }

    internal class DbEngineWorkerJobAccess(Db db) : IDbWorkerAccess
    {
        public Task<DbEngineWorkerJob?> GetJobAsync(string name, CancellationToken cancellationToken = default)
            => db.Workers.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }
}
