using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Backend.Core.Exceptions;
using RpgAdventureGame.Common.Enums;
using RpgAdventureGame.Database.SQLite.Entities.Character;

namespace RpgAdventureGame.Backend.CQRS.Queries.Character.GetDetails
{
    public class GetCharacterDetailsQueryHandler(IDbCharacterAccess characterAccess)
        : IRequestHandler<GetCharacterDetailsQuery, GetCharacterDetailsQueryResult>
    {
        public async ValueTask<GetCharacterDetailsQueryResult> Handle(GetCharacterDetailsQuery request)
        {
            var character = await characterAccess.Get(request.CharacterId)
                ?? throw new NotFoundException();

            return new()
            {
                Id = character.Id,
                Name = character.Name,
                Level = character.Level,
                Experience = character.Experience,
                CurrentHealth = character.CurrentHealth,
                MaxHealth = character.MaxHealth,
                Location = character switch
                {
                    { CurrentArea: not null } => new()
                    {
                        Type = CharacterLocationType.Area,
                        Id = character.CurrentArea.Id,
                        Name = character.CurrentArea.Name
                    },
                    { CurrentPath: not null } => new()
                    {
                        Type = CharacterLocationType.Path,
                        Id = character.CurrentPath.Id,
                        Name = character.CurrentPath.Name
                    },
                    _ => null
                },
            };
        }
    }
}
