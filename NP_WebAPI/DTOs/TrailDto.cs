using NP_WebAPI.Models;
using System.ComponentModel.DataAnnotations;
using static NP_WebAPI.Models.Trail;

namespace NP_WebAPI.DTOs
{
    public class TrailDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }
        [Required]
        public string Elevation { get; set; }
        public DifficultyType Difficulty { get; set; }
        public int NationalParkId { get; set; }
        public NationalPark NationalPark { get; set; }
    }
}
