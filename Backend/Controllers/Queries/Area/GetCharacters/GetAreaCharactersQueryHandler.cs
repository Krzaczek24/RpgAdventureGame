using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Database.SQLite.Entities.Area;

namespace RpgAdventureGame.Backend.Controllers.Queries.Area.GetCharacters
{
    public class GetAreaCharactersQueryHandler(IDbAreaAccess areaAccess)
        : IRequestHandler<GetAreaCharactersQuery, GetAreaCharactersQueryResult>
    {
        public async ValueTask<GetAreaCharactersQueryResult> Handle(GetAreaCharactersQuery request)
        {
            var areaCharacters = await areaAccess.ListAreaCharacters(request.AreaId);
            return new() { Characters = areaCharacters };
        }
    }
}
