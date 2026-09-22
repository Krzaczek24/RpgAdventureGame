using Krzaq.MediatR.Interfaces;

namespace RpgAdventureGame.Backend.Controllers.Queries.Area.GetAvailablePaths
{
    public class GetAreaAvailablePathsQuery : IRequest<GetAreaAvailablePathsQueryResult>
    {
        public int AreaId { get; set; }
    }
}
