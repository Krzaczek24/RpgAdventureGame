using Microsoft.EntityFrameworkCore;

namespace RpgAdventureGame.Database.SQLite.Entities.Path
{
    public interface IDbPathAccess
    {
        ValueTask<bool> Exists(int id, CancellationToken cancellationToken = default);
    }

    internal class DbPathAccess(Db db) : IDbPathAccess
    {
        public async ValueTask<bool> Exists(int id, CancellationToken cancellationToken = default)
            => await db.Paths.AnyAsync(p => p.Id == id, cancellationToken);
    }
}
