using System.ComponentModel.DataAnnotations;

namespace MealMasterAPI.Models.Dtos
{
    public class CofirmPassDTO
    {
        [Required(ErrorMessage = "The password is obligatory")]
        [MinLength(8, ErrorMessage = "The minimum number of characters is 8.")]
        [MaxLength(250, ErrorMessage = "The maximum number of characters is 250.")]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmpassword { get; set; }

    }
}
