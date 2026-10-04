using Krzaq.MediatR.Implementations;
using Microsoft.AspNetCore.Mvc;
using RpgAdventureGame.Backend.CQRS.Queries.Area.List;
using RpgAdventureGame.WebApi.Core.Controllers;
using RpgAdventureGame.WebApi.CQRS.Queries.Area.GetAvailablePaths;
using RpgAdventureGame.WebApi.CQRS.Queries.Area.List;

namespace RpgAdventureGame.WebApi.Controllers
{
    public class AreaController(IMediator mediator) : ApiController
    {
        [HttpGet]
        public async ValueTask<ActionResult<ListAreasQueryResult>> GetList()
            => Ok(await mediator.Send(new ListAreasQuery()));

        [HttpGet("{id}/available-paths")]
        public async ValueTask<ActionResult<GetAreaAvailablePathsQueryResult>> GetAvailablePaths([FromRoute] int id)
            => Ok(await mediator.Send(new GetAreaAvailablePathsQuery() { AreaId = id }));
    }
}
