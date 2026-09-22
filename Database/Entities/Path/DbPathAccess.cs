using Microsoft.EntityFrameworkCore;
using RpgAdventureGame.Database.SQLite.Models.Path;

namespace RpgAdventureGame.Database.SQLite.Entities.Path
{
    public interface IDbPathAccess
    {
        ValueTask<bool> Exists(int startAreaId, int endAreaId);
        ValueTask<int> Create(InsertPathDto insertParams);
    }

    internal class DbPathAccess(Db db) : IDbPathAccess
    {
        public async ValueTask<bool> Exists(int startAreaId, int endAreaId)
        {
            return await db.Paths.AnyAsync(p => p.StartAreaId == startAreaId && p.EndAreaId == endAreaId);
        }

        public async ValueTask<int> Create(InsertPathDto insertParams)
        {
            var path = new DbPath
            {
                Name = insertParams.Name,
                StartAreaId = insertParams.StartAreaId,
                EndAreaId = insertParams.EndAreaId,
                Distance = insertParams.Distance,
                DangerLevel = insertParams.DangerLevel,
                DangerProbability = insertParams.DangerProbability,
            };
            await db.Paths.AddAsync(path);
            await db.SaveChangesAsync();
            return path.Id;
        }
    }
}
