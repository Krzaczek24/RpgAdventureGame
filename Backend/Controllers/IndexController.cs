using Microsoft.AspNetCore.Mvc;
using RpgAdventureGame.Backend.Core.Controllers;

namespace RpgAdventureGame.Backend.Controllers
{
    [Route("index")]
    public class IndexController : ApiController
    {
        [HttpGet("/")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RedirectResult RedirectToJson() => Redirect(Program.ENDPOINT);
    }
}
