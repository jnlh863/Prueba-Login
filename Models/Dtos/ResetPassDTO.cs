using System.ComponentModel.DataAnnotations;

namespace MealMasterAPI.Models.Dtos
{
    public class ResetPassDto
    {
        public string email { get; set; } = null!;
        public string token { get; set; } = null!;

        [Required(ErrorMessage = "The password is obligatory")]
        [MinLength(8, ErrorMessage = "The minimum number of characters is 8.")]
        [MaxLength(250, ErrorMessage = "The maximum number of characters is 250.")]
        public string password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmpassword { get; set; } = null!;

    }
}
