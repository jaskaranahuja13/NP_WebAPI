using System.ComponentModel.DataAnnotations.Schema;

namespace NP_WebAPI.Models
{
    public class Trail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Distance { get; set; }
        public string Elevation { get; set; }
        public DateTime  DateTime { get; set; }
        public enum DifficultyType {Easy,Moderate,Difficult}
        public DifficultyType Difficulty { get; set; }
        public int NationalParkId { get; set; }
        [ForeignKey("NationalParkId")]
        public NationalPark NationalPark { get; set; }
    }
}
