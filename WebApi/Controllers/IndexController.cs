using Microsoft.AspNetCore.Mvc;
using RpgAdventureGame.WebApi.Core.Controllers;

namespace RpgAdventureGame.WebApi.Controllers
{
    [Route("index")]
    public class IndexController : ApiController
    {
        [HttpGet("/")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RedirectResult RedirectToJson() => Redirect(Program.ENDPOINT);
    }
}
