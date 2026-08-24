using Krzaq.MediatR.Interfaces;

namespace RpgAdventureGame.Backend.Controllers.Commands.Character.Create
{
    public class CreateCharacterCommand : IRequest<CreateCharacterCommandResult>
    {
        public string Name { get; set; } = string.Empty;
        public int StartingAreaID { get; set; }
    }
}
