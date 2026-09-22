namespace RpgAdventureGame.Database.SQLite.Models.Area
{
    public class SelectAreaPathListItemDto
    {
        public string Name { get; set; }
        public SelectAreaPathListItemAreaDto EndArea { get; set; }
        public decimal Distance { get; set; }
        public int DangerLevel { get; set; }
        public decimal DangerProbability { get; set; }
    }

    public class SelectAreaPathListItemAreaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
