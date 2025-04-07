using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

    }
}
