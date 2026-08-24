using Krzaq.Attributes.ProducesResponse;
using Microsoft.AspNetCore.Mvc;
using RpgAdventureGame.Backend.Core.Errors;
using System.Net;

namespace RpgAdventureGame.Backend.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponse(HttpStatusCode.OK)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.BadRequest)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.Unauthorized)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.Forbidden)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.NotFound)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.Conflict)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.InternalServerError)]
    public class ApiController : ControllerBase
    {
    }
}
