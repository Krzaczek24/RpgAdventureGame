using Krzaq.MediatR.Interfaces;

namespace RpgAdventureGame.Backend.Controllers.Commands.Area.Create
{
    public class CreateAreaCommand : IRequest<CreateAreaCommandResult>
    {
        public string Name { get; set; } = string.Empty;
    }
}
