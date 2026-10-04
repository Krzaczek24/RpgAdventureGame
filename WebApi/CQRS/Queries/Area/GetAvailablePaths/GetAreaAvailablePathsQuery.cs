using Krzaq.MediatR.Interfaces;

namespace RpgAdventureGame.Backend.WebApi.CQRS.Queries.Area.GetAvailablePaths
{
    public class GetAreaAvailablePathsQuery : IRequest<GetAreaAvailablePathsQueryResult>
    {
        public required int AreaId { get; init; }
    }
}
