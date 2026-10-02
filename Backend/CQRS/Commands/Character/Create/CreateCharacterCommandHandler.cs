using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Database.SQLite.Entities.Character;

namespace RpgAdventureGame.Backend.CQRS.Commands.Character.Create
{
    public class CreateCharacterCommandHandler(IDbCharacterAccess characterAccess)
        : IRequestHandler<CreateCharacterCommand, CreateCharacterCommandResult>
    {
        public async ValueTask<CreateCharacterCommandResult> Handle(CreateCharacterCommand request)
        {
            int id = await characterAccess.Create(request.Name);
            return new CreateCharacterCommandResult { Id = id };
        }
    }
}
