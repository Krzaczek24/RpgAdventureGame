using RpgAdventureGame.Database.SQLite.Models.Area;

namespace RpgAdventureGame.Database.SQLite.Models.Character
{
    public class SelectCharacterDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SelectAreaDetailsDto CurrentArea { get; set; }
    }
}
