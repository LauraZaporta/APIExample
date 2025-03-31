using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class FilmDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int Year { get; set; }
    }
}