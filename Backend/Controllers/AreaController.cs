using Krzaq.MediatR.Implementations;
using Microsoft.AspNetCore.Mvc;
using RpgAdventureGame.Backend.Controllers.Commands.Area.Create;
using RpgAdventureGame.Backend.Controllers.Queries.Area.GetAvailablePaths;
using RpgAdventureGame.Backend.Controllers.Queries.Area.GetCharacters;
using RpgAdventureGame.Backend.Controllers.Queries.Area.List;
using RpgAdventureGame.Backend.Core.Controllers;

namespace RpgAdventureGame.Backend.Controllers
{
    public class AreaController(IMediator mediator) : ApiController
    {
        [HttpPost]
        public ValueTask<CreateAreaCommandResult> Create([FromBody] CreateAreaCommand request) => mediator.Send(request);

        [HttpGet]
        public ValueTask<ListAreasQueryResult> GetList() => mediator.Send(new ListAreasQuery());

        [HttpGet("{id}/characters")]
        public ValueTask<GetAreaCharactersQueryResult> GetCharacters([FromRoute] int id) => mediator.Send(new GetAreaCharactersQuery() { AreaId = id });

        [HttpGet("{id}/available-paths")]
        public ValueTask<GetAreaAvailablePathsQueryResult> GetAvailablePaths([FromRoute] int id) => mediator.Send(new GetAreaAvailablePathsQuery() { AreaId = id });
    }
}
