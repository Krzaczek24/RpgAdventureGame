using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Backend.Core.Exceptions;
using RpgAdventureGame.Database.SQLite.Entities.Area;

namespace RpgAdventureGame.Backend.Controllers.Queries.Area.GetDetails
{
    public class GetAreaDetailsQueryHandler(IDbAreaAccess areaAccess)
        : IRequestHandler<GetAreaDetailsQuery, GetAreaDetailsQueryResult>
    {
        public async ValueTask<GetAreaDetailsQueryResult> Handle(GetAreaDetailsQuery request)
        {
            var area = await areaAccess.Get(request.AreaId)
                ?? throw new NotFoundException();

            return new() { AreaDetails = area };
        }
    }
}
