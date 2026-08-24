using RpgAdventureGame.Database.SQLite.Models.Character;
using Microsoft.EntityFrameworkCore;

namespace RpgAdventureGame.Database.SQLite.Entities.Character
{
    public interface IDbCharacterAccess
    {
        ValueTask<bool> IsNameUsed(string name);
        ValueTask<int> Create(InsertCharacterDto insertParams);
        ValueTask<SelectCharacterDetailsDto?> Get(int id);
    }

    internal class DbCharacterAccess(Db db) : IDbCharacterAccess
    {
        public async ValueTask<bool> IsNameUsed(string name)
            => await db.Characters.AnyAsync(c => c.Name == name);

        public async ValueTask<int> Create(InsertCharacterDto insertParams)
        {
            var character = new DbCharacter
            {
                Name = insertParams.Name,
                CurrentAreaId = insertParams.CurrentAreaId
            };

            await db.Characters.AddAsync(character);
            await db.SaveChangesAsync();

            return character.Id;
        }

        public async ValueTask<SelectCharacterDetailsDto?> Get(int id)
        {
            var query = from c in db.Characters
                        join a in db.Areas on c.CurrentAreaId equals a.Id
                        where c.Id == id
                        select new SelectCharacterDetailsDto
                        {
                            Id = c.Id,
                            Name = c.Name,
                            CurrentArea = new()
                            {
                                Id = a.Id,
                                Name = a.Name,
                            },
                        };
            return await query.FirstOrDefaultAsync();
        }
    }
}
