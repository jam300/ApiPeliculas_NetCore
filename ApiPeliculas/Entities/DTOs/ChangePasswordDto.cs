using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Entities.DTOs
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Current password is required.")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "New password must be between 6 and 100 characters.")]
        public string NewPassword { get; set; }
    }
}
