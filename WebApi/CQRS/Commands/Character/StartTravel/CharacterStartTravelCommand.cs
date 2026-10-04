using Krzaq.MediatR.Interfaces;

namespace RpgAdventureGame.Backend.WebApi.CQRS.Commands.Character.StartTravel
{
    public class CharacterStartTravelCommand : IRequest<CharacterStartTravelCommandResult>
    {
        public required int CharacterId { get; init; }
        public required IList<int> PathIds { get; init; }
    }
}
