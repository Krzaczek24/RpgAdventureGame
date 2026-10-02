using Microsoft.EntityFrameworkCore;
using RpgAdventureGame.Common.Enums;
using RpgAdventureGame.Database.SQLite.Entities.Area;
using RpgAdventureGame.Database.SQLite.Entities.Path;
using RpgAdventureGame.Database.SQLite.Interfaces;

namespace RpgAdventureGame.Database.SQLite.Entities.Character
{
    public interface IDbCharacterAccess
    {
        ValueTask<bool> Exists(int id, CancellationToken cancellationToken = default);
        ValueTask<bool> IsNameUsed(string name, CancellationToken cancellationToken = default);
        ValueTask<int> Create(string name, CancellationToken cancellationToken = default);
        ValueTask<DbCharacter?> Get(int id, CancellationToken cancellationToken = default);
        ValueTask<IReadOnlySet<DbCharacter>> Search(ISearchCharacterParams searchParams, CancellationToken cancellationToken = default);
        ValueTask SetLocation(int id, int locationId, CharacterLocationType locationType, CancellationToken cancellationToken = default);
    }

    internal class DbCharacterAccess(Db db) : IDbCharacterAccess
    {
        public async ValueTask<bool> Exists(int id, CancellationToken cancellationToken = default)
            => await db.Characters.AnyAsync(c => c.Id == id, cancellationToken);

        public async ValueTask<bool> IsNameUsed(string name, CancellationToken cancellationToken = default)
            => await db.Characters.AnyAsync(c => EF.Functions.Like(c.Name, name), cancellationToken);

        public async ValueTask<int> Create(string name, CancellationToken cancellationToken = default)
        {
            var character = new DbCharacter { Name = name };

            await db.Characters.AddAsync(character, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);

            return character.Id;
        }

        public async ValueTask<DbCharacter?> Get(int id, CancellationToken cancellationToken = default)
        {
            var query = from c in db.Characters
                        where c.Id == id
                        select new DbCharacter
                        {
                            Id = c.Id,
                            Name = c.Name,
                            CurrentArea = c.CurrentArea == null ? null : new DbArea
                            {
                                Id = c.CurrentArea.Id,
                                Name = c.CurrentArea.Name,
                            },
                            CurrentPath = c.CurrentPath == null ? null : new DbPath
                            {
                                Id = c.CurrentPath.Id,
                                Name = c.CurrentPath.Name,
                            },
                        };

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async ValueTask<IReadOnlySet<DbCharacter>> Search(ISearchCharacterParams searchParams, CancellationToken cancellationToken = default)
        {
            var query = from c in db.Characters
                        where (EF.Functions.Like(c.Name, $"%{searchParams.Name}%"))
                        where (searchParams.AreaIds.Count == 0 || (c.CurrentAreaId.HasValue && searchParams.AreaIds.Contains(c.CurrentAreaId.Value)))
                        where (searchParams.PathIds.Count == 0 || (c.CurrentPathId.HasValue && searchParams.PathIds.Contains(c.CurrentPathId.Value)))
                        where (searchParams.MinLevel == null || c.Level >= searchParams.MinLevel)
                        where (searchParams.MaxLevel == null || c.Level <= searchParams.MaxLevel)
                        select new DbCharacter
                        {
                            Id = c.Id,
                            Name = c.Name,
                            CurrentArea = c.CurrentArea == null ? null : new DbArea
                            {
                                Id = c.CurrentArea.Id,
                                Name = c.CurrentArea.Name,
                            },
                            CurrentPath = c.CurrentPath == null ? null : new DbPath
                            {
                                Id = c.CurrentPath.Id,
                                Name = c.CurrentPath.Name,
                            },
                        };

            return (await query.ToHashSetAsync(cancellationToken)).AsReadOnly();
        }

        public async ValueTask SetLocation(int id, int locationId, CharacterLocationType locationType, CancellationToken cancellationToken = default)
        {
            var character = await Get(id, cancellationToken)
                ?? throw new InvalidOperationException($"Character with id {id} not found.");

            (character.CurrentAreaId, character.CurrentPathId) = locationType switch
            {
                CharacterLocationType.Area => ((int?)locationId, (int?)null),
                CharacterLocationType.Path => (null, locationId),
                _ => throw new ArgumentOutOfRangeException(nameof(locationType), locationType, null)
            };

            db.Update(character);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
