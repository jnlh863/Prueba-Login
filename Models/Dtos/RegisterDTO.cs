using System.ComponentModel.DataAnnotations;

namespace MealMasterAPI.Models.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "The username is obligatory")]
        [MaxLength(50, ErrorMessage = "The maximum number of characters is 50.")]
        public string username { get; set; } = null!;

        [Required(ErrorMessage = "The email is obligatory")]
        [EmailAddress(ErrorMessage = "The email is obligatory.")]
        public string email { get; set; } = null!;

        [Required(ErrorMessage = "The password is obligatory")]
        [MinLength(8, ErrorMessage = "The minimum number of characters is 8.")]
        [MaxLength(250, ErrorMessage = "The maximum number of characters is 250.")]
        public string password { get; set; } = null!;

    }
}
