using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Database.SQLite.Entities.Area;

namespace RpgAdventureGame.Backend.CQRS.Queries.Area.GetAvailablePaths
{
    public class GetAreaAvailablePathsQueryHandler(IDbAreaAccess areaAccess)
        : IRequestHandler<GetAreaAvailablePathsQuery, GetAreaAvailablePathsQueryResult>
    {
        public async ValueTask<GetAreaAvailablePathsQueryResult> Handle(GetAreaAvailablePathsQuery request)
        {
            var areaPaths = await areaAccess.ListOutgoingPaths(request.AreaId);
            return new()
            {
                AvailablePaths = areaPaths.Select(path =>new AvailablePathDto
                {
                    Id = path.Id,
                    Name = path.Name,
                    TargetArea = new()
                    {
                        Id = path.EndArea.Id,
                        Name = path.EndArea.Name,
                    },
                }).ToHashSet().AsReadOnly(),
            };
        }
    }
}
