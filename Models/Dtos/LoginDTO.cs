using System.ComponentModel.DataAnnotations;

namespace MealMasterAPI.Models.Dtos
{
    public class LoginDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "The maximum number of characters is 50.")]
        public string email { get; set; } = null!;

        [Required]
        [MaxLength(50, ErrorMessage = "The maximum number of characters is 50.")]
        public string password { get; set; } = null!;

    }
}
