using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Backend.Common.Enums;
using System.Text.Json.Serialization;

namespace RpgAdventureGame.Backend.WebApi.CQRS.Commands.Character.SetLocation
{
    public class SetCharacterLocationCommand : IRequest
    {
        [JsonIgnore]
        public int CharacterId { get; init; }
        public int LocationId { get; init; }
        public CharacterLocationType LocationType { get; init; }
    }
}
