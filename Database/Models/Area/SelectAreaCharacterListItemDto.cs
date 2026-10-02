namespace RpgAdventureGame.Database.SQLite.Models.Area
{
    public class SelectAreaCharacterListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"({Id}) {Name}";
    }
}
