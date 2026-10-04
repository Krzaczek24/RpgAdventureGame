using Microsoft.EntityFrameworkCore;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Area;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Path;
using RpgAdventureGame.Backend.Database.SQLite.Interfaces;

namespace RpgAdventureGame.Backend.Database.SQLite.Entities.Character
{
    public interface IDbCharacterAccess
    {
        ValueTask<bool> CharacterExistsAsync(int characterId, CancellationToken cancellationToken = default);
        ValueTask<bool> IsCharacterNameUsedAsync(string characterName, CancellationToken cancellationToken = default);
        ValueTask<bool> IsCharacterTravelingAsync(int characterId, CancellationToken cancellationToken = default);
        ValueTask<int> CreateCharacterAsync(string characterName, CancellationToken cancellationToken = default);
        ValueTask<DbCharacter?> GetCharacterAsync(int characterId, CancellationToken cancellationToken = default);
        ValueTask<IReadOnlySet<DbCharacter>> SearchCharactersAsync(ISearchCharacterParams searchParams, CancellationToken cancellationToken = default);
        ValueTask SetCharacterCurrentAreaAsync(int characterId, int areaId, CancellationToken cancellationToken = default);
        ValueTask SetCharacterCurrentPathAsync(int characterId, int pathId, CancellationToken cancellationToken = default);
    }

    internal class DbCharacterAccess(Db db) : IDbCharacterAccess
    {
        public async ValueTask<bool> CharacterExistsAsync(int characterId, CancellationToken cancellationToken = default)
            => await db.Characters.AnyAsync(c => c.Id == characterId, cancellationToken);

        public async ValueTask<bool> IsCharacterNameUsedAsync(string characterName, CancellationToken cancellationToken = default)
            => await db.Characters.AnyAsync(c => EF.Functions.Like(c.Name, characterName), cancellationToken);

        public async ValueTask<bool> IsCharacterTravelingAsync(int characterId, CancellationToken cancellationToken = default)
            => await db.Characters.AnyAsync(c => c.Id == characterId && (c.CurrentTravel != null || c.CurrentTravel.Completed), cancellationToken);

        public async ValueTask<int> CreateCharacterAsync(string characterName, CancellationToken cancellationToken = default)
        {
            var character = new DbCharacter { Name = characterName };

            await db.Characters.AddAsync(character, cancellationToken);
            await db.SaveChangesAsync(cancellationToken);

            return character.Id;
        }

        public async ValueTask<DbCharacter?> GetCharacterAsync(int characterId, CancellationToken cancellationToken = default)
        {
            var query = from c in db.Characters
                        where c.Id == characterId
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

        public async ValueTask<IReadOnlySet<DbCharacter>> SearchCharactersAsync(ISearchCharacterParams searchParams, CancellationToken cancellationToken = default)
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

        public async ValueTask SetCharacterCurrentAreaAsync(int characterId, int areaId, CancellationToken cancellationToken = default)
            => await SetCurrentLocation(characterId, areaId, null, cancellationToken);

        public async ValueTask SetCharacterCurrentPathAsync(int characterId, int pathId, CancellationToken cancellationToken = default)
            => await SetCurrentLocation(characterId, null, pathId, cancellationToken);

        private async ValueTask SetCurrentLocation(int characterId, int? areaId, int? pathId, CancellationToken cancellationToken = default)
        {
            int affectedRows = await db.Characters
                .Where(c => c.Id == characterId)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(c => c.CurrentAreaId, areaId)
                    .SetProperty(c => c.CurrentPathId, pathId)
                , cancellationToken);
        }
    }
}
