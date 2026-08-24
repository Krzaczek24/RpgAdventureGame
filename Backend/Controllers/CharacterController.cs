using Krzaq.MediatR.Implementations;
using Microsoft.AspNetCore.Mvc;
using RpgAdventureGame.Backend.Controllers.Commands.Character.Create;
using RpgAdventureGame.Backend.Controllers.Queries.Character.GetDetails;
using RpgAdventureGame.Backend.Core.Controllers;

namespace RpgAdventureGame.Backend.Controllers
{
    public class CharacterController(IMediator mediator) : ApiController
    {
        [HttpPost]
        public ValueTask<CreateCharacterCommandResult> Create([FromBody] CreateCharacterCommand request) => mediator.Send(request);

        [HttpGet("{id}")]
        public ValueTask<GetCharacterDetailsQueryResult> GetDetails([FromRoute] int id) => mediator.Send(new GetCharacterDetailsQuery() { CharacterId = id });
    }
}
