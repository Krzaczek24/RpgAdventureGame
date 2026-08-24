using Krzaq.MediatR.Interfaces;

namespace RpgAdventureGame.Backend.Controllers.Queries.Area.GetDetails
{
    public class GetAreaDetailsQuery : IRequest<GetAreaDetailsQueryResult>
    {
        public int AreaId { get; set; }
    }
}
