using Krzaq.MediatR.Implementations;
using Microsoft.AspNetCore.Mvc;
using RpgAdventureGame.Backend.Core.Controllers;
using RpgAdventureGame.Backend.CQRS.Queries.Area.GetAvailablePaths;
using RpgAdventureGame.Backend.CQRS.Queries.Area.List;

namespace RpgAdventureGame.Backend.Controllers
{
    public class AreaController(IMediator mediator) : ApiController
    {
        [HttpGet]
        public ValueTask<ListAreasQueryResult> GetList() => mediator.Send(new ListAreasQuery());

        [HttpGet("{id}/available-paths")]
        public ValueTask<GetAreaAvailablePathsQueryResult> GetAvailablePaths([FromRoute] int id) => mediator.Send(new GetAreaAvailablePathsQuery() { AreaId = id });
    }
}
