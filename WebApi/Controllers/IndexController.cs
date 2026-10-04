using Microsoft.AspNetCore.Mvc;
using RpgAdventureGame.Backend.WebApi.Core.Controllers;

namespace RpgAdventureGame.Backend.WebApi.Controllers
{
    [Route("index")]
    public class IndexController : ApiController
    {
        [HttpGet("/")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RedirectResult RedirectToJson() => Redirect(Program.ENDPOINT);
    }
}
