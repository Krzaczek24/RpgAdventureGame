using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Backend.Core.Errors;
using RpgAdventureGame.Backend.Core.Exceptions;
using RpgAdventureGame.Backend.Core.Extensions;
using RpgAdventureGame.Database.SQLite.Entities.Character;

namespace RpgAdventureGame.Backend.Controllers.Commands.Character.Create
{
    public class CreateCharacterCommandHandler(IDbCharacterAccess characterAccess) : IRequestHandler<CreateCharacterCommand, CreateCharacterCommandResult>
    {
        public async ValueTask<CreateCharacterCommandResult> Handle(CreateCharacterCommand request)
        {
            if (await characterAccess.IsNameUsed(request.Name))
                throw new ConflictException([ErrorCode.NotFound.AsFieldError(() => request.Name)]);

            int id = await characterAccess.Create(new()
            {
                Name = request.Name,
                CurrentAreaId = request.StartingAreaID,
            });
            return new CreateCharacterCommandResult { Id = id };
        }
    }
}
