using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Database.SQLite.Entities.Area;

namespace RpgAdventureGame.Backend.Controllers.Queries.Area.List
{
    public class ListAreasQueryHandler(IDbAreaAccess areaAccess)
        : IRequestHandler<ListAreasQuery, ListAreasQueryResult>
    {
        public async ValueTask<ListAreasQueryResult> Handle(ListAreasQuery request)
        {
            var areas = await areaAccess.List();

            return new() { Areas = areas };
        }
    }
}
