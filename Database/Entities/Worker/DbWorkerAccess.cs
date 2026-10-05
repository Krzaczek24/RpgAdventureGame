using Microsoft.EntityFrameworkCore;

namespace RpgAdventureGame.Backend.Database.SQLite.Entities.Worker
{
    public interface IDbWorkerAccess
    {
        Task<DbWorker?> GetWorkerAsync(string name, CancellationToken cancellationToken = default);
    }

    internal class DbWorkerAccess(Db db) : IDbWorkerAccess
    {
        public Task<DbWorker?> GetWorkerAsync(string name, CancellationToken cancellationToken = default)
            => db.Workers.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }
}
