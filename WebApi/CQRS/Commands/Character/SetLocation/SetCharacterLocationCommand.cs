using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Common.Enums;
using System.Text.Json.Serialization;

namespace RpgAdventureGame.WebApi.CQRS.Commands.Character.SetLocation
{
    public class SetCharacterLocationCommand : IRequest
    {
        [JsonIgnore]
        public int CharacterId { get; init; }
        public int LocationId { get; init; }
        public CharacterLocationType LocationType { get; init; }
    }
}
