using Krzaq.MediatR.Interfaces;

namespace RpgAdventureGame.WebApi.CQRS.Queries.Area.GetAvailablePaths
{
    public class GetAreaAvailablePathsQuery : IRequest<GetAreaAvailablePathsQueryResult>
    {
        public required int AreaId { get; init; }
    }
}
