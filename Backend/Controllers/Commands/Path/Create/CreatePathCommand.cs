using Krzaq.MediatR.Interfaces;

namespace RpgAdventureGame.Backend.Controllers.Commands.Path.Create
{
    public class CreatePathCommand : IRequest<CreatePathCommandResult>
    {
        public string Name { get; set; } = string.Empty;
        public int StartAreaId { get; set; }
        public int EndAreaId { get; set; }
        public decimal Distance { get; set; }
        public int DangerLevel { get; set; }
        public decimal DangerProbability { get; set; }
        public bool BothWays { get; set; }
    }
}
