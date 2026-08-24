using Krzaq.MediatR.Interfaces;
using RpgAdventureGame.Backend.Core.Errors;
using RpgAdventureGame.Backend.Core.Exceptions;
using RpgAdventureGame.Backend.Core.Extensions;
using RpgAdventureGame.Database.SQLite.Entities.Area;
using RpgAdventureGame.Database.SQLite.Models.Area;

namespace RpgAdventureGame.Backend.Controllers.Commands.Area.Create
{
    public class CreateAreaCommandHandler(IDbAreaAccess areaAccess) : IRequestHandler<CreateAreaCommand, CreateAreaCommandResult>
    {
        public async ValueTask<CreateAreaCommandResult> Handle(CreateAreaCommand request)
        {
            if (await areaAccess.IsNameUsed(request.Name))
                throw new ConflictException([ErrorCode.NotFound.AsFieldError(() => request.Name)]);

            int id = await areaAccess.Create(new InsertAreaDto { Name = request.Name });
            return new CreateAreaCommandResult { Id = id };
        }
    }
}
