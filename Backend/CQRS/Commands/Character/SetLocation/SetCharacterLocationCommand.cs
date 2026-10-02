using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Common.Enums;

namespace RpgAdventureGame.Backend.CQRS.Commands.Character.SetLocation
{
    public class SetCharacterLocationCommand : IRequest
    {
        public required int CharacterId { get; init; }
        public required int LocationId { get; init; }
        public required CharacterLocationType LocationType { get; init; }
    }
}
