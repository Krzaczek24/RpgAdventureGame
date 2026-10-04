using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Backend.Database.SQLite.Entities.Area;

namespace RpgAdventureGame.Backend.WebApi.CQRS.Queries.Area.GetAvailablePaths
{
    public class GetAreaAvailablePathsQueryHandler(IDbAreaAccess areaAccess)
        : IRequestHandler<GetAreaAvailablePathsQuery, GetAreaAvailablePathsQueryResult>
    {
        public async ValueTask<GetAreaAvailablePathsQueryResult> Handle(GetAreaAvailablePathsQuery request, CancellationToken cancellationToken = default)
        {
            var areaPaths = await areaAccess.ListAreaOutgoingPathsAsync(request.AreaId, cancellationToken);
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
