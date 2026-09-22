using Microsoft.EntityFrameworkCore;
using RpgAdventureGame.Database.SQLite.Models.Area;
using System.Collections.ObjectModel;

namespace RpgAdventureGame.Database.SQLite.Entities.Area
{
    public interface IDbAreaAccess
    {
        ValueTask<bool> IsNameUsed(string name);
        ValueTask<bool> Exists(int id);
        ValueTask<int> Create(InsertAreaDto insertParams);
        ValueTask<ReadOnlySet<SelectAreaListItemDto>> List();
        ValueTask<ReadOnlySet<SelectAreaCharacterListItemDto>> ListAreaCharacters(int areaId);
        ValueTask<ReadOnlySet<SelectAreaPathListItemDto>> ListAreaPaths(int areaId);
    }

    internal class DbAreaAccess(Db db) : IDbAreaAccess
    {
        public async ValueTask<bool> IsNameUsed(string name)
            => await db.Areas.AnyAsync(a => a.Name == name);

        public async ValueTask<bool> Exists(int id)
            => await db.Areas.AnyAsync(a => a.Id == id);

        public async ValueTask<int> Create(InsertAreaDto insertParams)
        {
            var area = new DbArea
            {
                Name = insertParams.Name,
            };

            await db.Areas.AddAsync(area);
            await db.SaveChangesAsync();

            return area.Id;
        }

        public async ValueTask<ReadOnlySet<SelectAreaListItemDto>> List()
        {
            var query = from a in db.Areas
                        select new SelectAreaListItemDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                        };
            var result = await query.ToHashSetAsync();
            return result.AsReadOnly();
        }

        public async ValueTask<ReadOnlySet<SelectAreaCharacterListItemDto>> ListAreaCharacters(int areaId)
        {
            var query = from c in db.Characters
                        where c.CurrentAreaId == areaId
                        select new SelectAreaCharacterListItemDto
                        {
                            Id = c.Id,
                            Name = c.Name,
                        };
            var result = await query.ToHashSetAsync();
            return result.AsReadOnly();
        }

        public async ValueTask<ReadOnlySet<SelectAreaPathListItemDto>> ListAreaPaths(int areaId)
        {
            var query = from p in db.Paths
                        where p.StartAreaId == areaId
                        select new SelectAreaPathListItemDto
                        {
                            Name = p.Name,
                            EndArea = new SelectAreaPathListItemAreaDto
                            {
                                Id = p.EndArea.Id,
                                Name = p.EndArea.Name,
                            },
                            Distance = p.Distance,
                            DangerLevel = p.DangerLevel,
                            DangerProbability = p.DangerProbability,
                        };
            var result = await query.ToHashSetAsync();
            return result.AsReadOnly();
        }
    }
}
