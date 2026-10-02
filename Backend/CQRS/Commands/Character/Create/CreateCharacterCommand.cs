using Krzaq.MediatR.Interfaces;

namespace RpgAdventureGame.Backend.CQRS.Commands.Character.Create
{
    public class CreateCharacterCommand : IRequest<CreateCharacterCommandResult>
    {
        public required string Name { get; init; }
    }
}
