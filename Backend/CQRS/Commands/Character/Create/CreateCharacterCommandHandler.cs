using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Database.SQLite.Entities.Character;

namespace RpgAdventureGame.Backend.CQRS.Commands.Character.Create
{
    public class CreateCharacterCommandHandler(IDbCharacterAccess characterAccess)
        : IRequestHandler<CreateCharacterCommand, CreateCharacterCommandResult>
    {
        public async ValueTask<CreateCharacterCommandResult> Handle(CreateCharacterCommand request, CancellationToken cancellationToken = default)
        {
            int id = await characterAccess.CreateCharacterAsync(request.Name, cancellationToken);
            return new CreateCharacterCommandResult { Id = id };
        }
    }
}
