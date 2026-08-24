using Krzaq.MediatR.Interfaces;

namespace RpgAdventureGame.Backend.Controllers.Queries.Character.GetDetails
{
    public class GetCharacterDetailsQuery : IRequest<GetCharacterDetailsQueryResult>
    {
        public int CharacterId { get; set; }
    }
}
