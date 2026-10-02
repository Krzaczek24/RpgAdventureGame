using Microsoft.EntityFrameworkCore;
using RpgAdventureGame.Database.SQLite.Entities.Path;

namespace RpgAdventureGame.Database.SQLite.Entities.Area
{
    public interface IDbAreaAccess
    {
        ValueTask<bool> Exists(int id, CancellationToken cancellationToken = default);
        ValueTask<IReadOnlySet<DbArea>> List(CancellationToken cancellationToken = default);
        ValueTask<IReadOnlySet<DbPath>> ListOutgoingPaths(int areaId, CancellationToken cancellationToken = default);
    }

    internal class DbAreaAccess(Db db) : IDbAreaAccess
    {
        public async ValueTask<bool> Exists(int id, CancellationToken cancellationToken = default)
            => await db.Areas.AnyAsync(a => a.Id == id, cancellationToken);

        public async ValueTask<IReadOnlySet<DbArea>> List(CancellationToken cancellationToken = default)
        {
            var query = from a in db.Areas
                        select new DbArea
                        {
                            Id = a.Id,
                            Name = a.Name,
                        };

            return (await query.ToHashSetAsync(cancellationToken)).AsReadOnly();
        }

        public async ValueTask<IReadOnlySet<DbPath>> ListOutgoingPaths(int areaId, CancellationToken cancellationToken = default)
        {
            var query = from p in db.Paths
                        where p.StartAreaId == areaId
                        select new DbPath
                        {
                            Name = p.Name,
                            EndArea = new DbArea
                            {
                                Id = p.EndArea.Id,
                                Name = p.EndArea.Name,
                            },
                            Distance = p.Distance,
                            DangerLevel = p.DangerLevel,
                            DangerProbability = p.DangerProbability,
                        };

            return (await query.ToHashSetAsync(cancellationToken)).AsReadOnly();
        }
    }
}
