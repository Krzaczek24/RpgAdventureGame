using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Backend.Core.Errors;
using RpgAdventureGame.Backend.Core.Exceptions;
using RpgAdventureGame.Backend.Core.Extensions;
using RpgAdventureGame.Database.SQLite.Entities.Area;
using RpgAdventureGame.Database.SQLite.Entities.Path;

namespace RpgAdventureGame.Backend.Controllers.Commands.Path.Create
{
    public class CreatePathCommandHandler(IDbAreaAccess areaAccess, IDbPathAccess pathAccess)
        : IRequestHandler<CreatePathCommand, CreatePathCommandResult>
    {
        public async ValueTask<CreatePathCommandResult> Handle(CreatePathCommand request)
        {
            if (!await areaAccess.Exists(request.StartAreaId))
                throw new BadRequestException([ErrorCode.NotFound.AsFieldError(() => request.StartAreaId)]);

            if (!await areaAccess.Exists(request.EndAreaId))
                throw new BadRequestException([ErrorCode.NotFound.AsFieldError(() => request.EndAreaId)]);

            if (await pathAccess.Exists(request.StartAreaId, request.EndAreaId))
                throw new ConflictException([
                    ErrorCode.NonUnique.AsFieldError(() => request.StartAreaId), 
                    ErrorCode.NonUnique.AsFieldError(() => request.EndAreaId)]);

            if (request.BothWays)
            {
                if (await pathAccess.Exists(request.EndAreaId, request.StartAreaId))
                    throw new ConflictException([
                        ErrorCode.NonUnique.AsFieldError(() => request.EndAreaId),
                        ErrorCode.NonUnique.AsFieldError(() => request.StartAreaId)]);
            }

            var ids = new List<int>
            {
                await pathAccess.Create(new()
                {
                    Name = request.Name,
                    StartAreaId = request.StartAreaId,
                    EndAreaId = request.EndAreaId,
                    Distance = request.Distance,
                    DangerLevel = request.DangerLevel,
                    DangerProbability = request.DangerProbability,
                })
            };

            if (request.BothWays)
            {
                ids.Add(await pathAccess.Create(new()
                {
                    Name = request.Name,
                    StartAreaId = request.EndAreaId,
                    EndAreaId = request.StartAreaId,
                    Distance = request.Distance,
                    DangerLevel = request.DangerLevel,
                    DangerProbability = request.DangerProbability,
                }));
            }

            return new CreatePathCommandResult { Id = ids.AsReadOnly() };
        }
    }
}
