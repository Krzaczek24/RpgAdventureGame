using System.Collections.ObjectModel;

namespace RpgAdventureGame.Backend.Controllers.Commands.Path.Create
{
    public class CreatePathCommandResult
    {
        public required ReadOnlyCollection<int> Id { get; set; }
    }
}
