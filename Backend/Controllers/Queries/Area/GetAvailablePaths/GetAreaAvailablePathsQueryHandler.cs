using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Database.SQLite.Entities.Area;

namespace RpgAdventureGame.Backend.Controllers.Queries.Area.GetAvailablePaths
{
    public class GetAreaAvailablePathsQueryHandler(IDbAreaAccess areaAccess)
        : IRequestHandler<GetAreaAvailablePathsQuery, GetAreaAvailablePathsQueryResult>
    {
        public async ValueTask<GetAreaAvailablePathsQueryResult> Handle(GetAreaAvailablePathsQuery request)
        {
            var areaPaths = await areaAccess.ListAreaPaths(request.AreaId);
            return new() { Paths = areaPaths };
        }
    }
}
