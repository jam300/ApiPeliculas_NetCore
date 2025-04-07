using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Entities.Dtos
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no debe de revasar los 100 caracteres")]
        public string Name { get; set; }
    }
}
