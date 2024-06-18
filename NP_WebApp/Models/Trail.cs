using System.ComponentModel.DataAnnotations;

namespace NP_WebApp.Models
{
    public class Trail
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }
        [Required]
        public string Elevation { get; set; }
        public enum DifficultyType { easy,Moderate,Difficult }
        [Required]
        public DifficultyType Difficulty { get; set; }
        [Display(Name ="National Park")]
        public int NationalParkId { get; set; }
        public NationalPark NationalPark { get; set; }
    }
}
