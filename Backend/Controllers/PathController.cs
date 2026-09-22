using Krzaq.MediatR.Implementations;
using Microsoft.AspNetCore.Mvc;
using RpgAdventureGame.Backend.Controllers.Commands.Path.Create;
using RpgAdventureGame.Backend.Core.Controllers;

namespace RpgAdventureGame.Backend.Controllers
{
    public class PathController(IMediator mediator) : ApiController
    {
        [HttpPost]
        public ValueTask<CreatePathCommandResult> Create([FromBody] CreatePathCommand request) => mediator.Send(request);
    }
}
