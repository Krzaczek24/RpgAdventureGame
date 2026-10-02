namespace RpgAdventureGame.Database.SQLite.Models.Character
{
    public class SelectCharacterDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SelectCharacterDetailsAreaDetailsDto CurrentArea { get; set; }

        public override string ToString() => $"({Id}) {Name}";
    }

    public class SelectCharacterDetailsAreaDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"({Id}) {Name}";
    }
}
