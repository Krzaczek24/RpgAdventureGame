using Krzaq.MediatR.Interfaces;

namespace RpgAdventureGame.Backend.Controllers.Queries.Area.GetCharacters
{
    public class GetAreaCharactersQuery : IRequest<GetAreaCharactersQueryResult>
    {
        public int AreaId { get; set; }
    }
}
