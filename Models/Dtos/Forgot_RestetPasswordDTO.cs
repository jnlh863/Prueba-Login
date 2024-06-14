using System.ComponentModel.DataAnnotations;

namespace MealMasterAPI.Models.Dtos
{
    public class Forgot_RestetPasswordDTO
    {
        public string token { get; set; }

        [StringLength(100)]
        public string new_password { get; set; } = null!;

    }
}
