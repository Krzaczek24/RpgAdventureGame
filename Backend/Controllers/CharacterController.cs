using Krzaq.MediatR.Implementations;
using Microsoft.AspNetCore.Mvc;
using RpgAdventureGame.Common.Enums;
using RpgAdventureGame.Backend.Core.Controllers;
using RpgAdventureGame.Backend.CQRS.Commands.Character.Create;
using RpgAdventureGame.Backend.CQRS.Commands.Character.SetLocation;
using RpgAdventureGame.Backend.CQRS.Queries.Character.GetDetails;
using RpgAdventureGame.Backend.CQRS.Queries.Character.Search;

namespace RpgAdventureGame.Backend.Controllers
{
    public class CharacterController(IMediator mediator) : ApiController
    {
        [HttpPost]
        public ValueTask<CreateCharacterCommandResult> Create([FromBody] CreateCharacterCommand request)
            => mediator.Send(request);

        [HttpGet("{id}")]
        public ValueTask<GetCharacterDetailsQueryResult> GetDetails([FromRoute] int id)
            => mediator.Send(new GetCharacterDetailsQuery() { CharacterId = id });

        [HttpGet("search")]
        public ValueTask<SearchCharactersQueryResult> Search([FromQuery] SearchCharactersQuery request)
            => mediator.Send(request);

        [HttpPatch("{id}/location")]
        public ValueTask SetLocation([FromRoute] int id, [FromQuery] int locationId, [FromQuery] CharacterLocationType locationType)
            => mediator.Send(new SetCharacterLocationCommand { CharacterId = id, LocationId = locationId, LocationType = locationType });
    }
}
