using System.ComponentModel.DataAnnotations;

namespace MealMasterAPI.Models.Dtos
{
    public class LoginDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "The maximum number of characters is 50.")]
        public string email { get; set; } = null!;

        [Required]
        [MaxLength(50, ErrorMessage = "The maximum number of characters is 50.")]
        public string password { get; set; } = null!;

    }
}
