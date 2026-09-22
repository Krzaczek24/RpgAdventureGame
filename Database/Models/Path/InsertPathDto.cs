namespace RpgAdventureGame.Database.SQLite.Models.Path
{
    public class InsertPathDto
    {
        public string Name { get; set; }
        public int StartAreaId { get; set; }
        public int EndAreaId { get; set; }
        public decimal Distance { get; set; }
        public int DangerLevel { get; set; }
        public decimal DangerProbability { get; set; }
    }
}
