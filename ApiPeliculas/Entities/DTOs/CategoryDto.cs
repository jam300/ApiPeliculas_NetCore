using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Entities.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no debe de revasar los 100 caracteres")]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
