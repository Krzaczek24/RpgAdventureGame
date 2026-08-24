using RpgAdventureGame.Database.SQLite.Models.Area;
using Microsoft.EntityFrameworkCore;

namespace RpgAdventureGame.Database.SQLite.Entities.Area
{
    public interface IDbAreaAccess
    {
        ValueTask<bool> IsNameUsed(string name);
        ValueTask<int> Create(InsertAreaDto insertParams);
        ValueTask<SelectAreaDetailsDto?> Get(int id);
    }

    internal class DbAreaAccess(Db db) : IDbAreaAccess
    {
        public async ValueTask<bool> IsNameUsed(string name)
            => await db.Areas.AnyAsync(c => c.Name == name);

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

        public async ValueTask<SelectAreaDetailsDto?> Get(int id)
        {
            var query = from a in db.Areas
                        where a.Id == id
                        select new SelectAreaDetailsDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                        };
            return await query.FirstOrDefaultAsync();
        }
    }
}
