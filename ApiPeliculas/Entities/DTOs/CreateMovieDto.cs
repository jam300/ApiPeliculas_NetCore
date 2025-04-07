using ApiPeliculas.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Entities.DTOs
{
    public class CreateMovieDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int Duration { get; set; }
        public string ImagenPath { get; set; }
        [Required]
        public MovieClassification Classification { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
