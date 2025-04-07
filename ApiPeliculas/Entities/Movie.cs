using ApiPeliculas.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ApiPeliculas.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string ImagenPath { get; set; }
        public MovieClassification Classification { get; set; }
        public DateTime CreatedAt { get; set; }

        //ForeignKey to Categories table

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}
