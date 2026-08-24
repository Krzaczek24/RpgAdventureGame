using Krzaq.MediatR.Implementations;
using Microsoft.AspNetCore.Mvc;
using RpgAdventureGame.Backend.Controllers.Commands.Area.Create;
using RpgAdventureGame.Backend.Controllers.Queries.Area.GetDetails;
using RpgAdventureGame.Backend.Core.Controllers;

namespace RpgAdventureGame.Backend.Controllers
{
    public class AreaController(IMediator mediator) : ApiController
    {
        [HttpPost]
        public ValueTask<CreateAreaCommandResult> Create([FromBody] CreateAreaCommand request) => mediator.Send(request);

        [HttpGet("{id}")]
        public ValueTask<GetAreaDetailsQueryResult> GetDetails([FromRoute] int id) => mediator.Send(new GetAreaDetailsQuery() { AreaId = id });
    }
}
